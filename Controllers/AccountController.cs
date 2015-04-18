using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BootstrapVillas.Filters;
using BootstrapVillas.Models;
using IsolationLevel = System.Transactions.IsolationLevel;
using System.IO;
using System.Web.Routing;

namespace BootstrapVillas.Controllers
{

    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        private UsersContext userContext = new UsersContext();

        public bool UpdateCustomer(Customer customer)
        {
            Customer previousCustomer = (Customer)Session["prc_previouscustomer"];
            customer.CustomerID = previousCustomer.CustomerID;

            if ((!CheckIfCustomerEmailAlreadyExists(customer.EmailAddress) &&
                !CheckIfUserExistsInSimpleMemberProvider(customer.EmailAddress)) || (previousCustomer.EmailAddress.ToLower().Trim() == customer.EmailAddress.ToLower().Trim()))
            {

                PortugalVillasContext _dbContext = new PortugalVillasContext();
                UsersContext _usersContext = new UsersContext();

                //get the old customer detailf from the session



                //update simplemembership provider too
                var user =
                    _usersContext.UserProfiles.Where(
                        x => x.UserName.ToLower().Trim() == previousCustomer.EmailAddress.ToLower().Trim())
                        .FirstOrDefault();

                user.UserName = customer.EmailAddress;

                //update customer and user
                try
                {
                    if (ModelState.IsValid)
                    {
                        _dbContext.Entry(customer).State = EntityState.Modified;
                        var objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;

                        var refreshableObjects = (from entry in objContext.ObjectStateManager.GetObjectStateEntries(
                            EntityState.Added
                            | EntityState.Deleted
                            | EntityState.Modified
                            | EntityState.Unchanged)
                                                  where entry.EntityKey != null
                                                  select entry.Entity);

                        objContext.Refresh(RefreshMode.ClientWins, refreshableObjects);

                        //if it works, do the update for the userContext, else, don't as it failed
                        if (objContext.SaveChanges() > 0)
                        {
                            _usersContext.Entry(user).State = EntityState.Modified;
                            _usersContext.SaveChanges();

                            WebSecurity.Logout();

                            Session["prc_customer"] = customer;
                            //update the customer
                            return true;
                        }

                    }
                }

                catch
            (DbUpdateConcurrencyException ex)
                {
                    var objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
                    var entry = ex.Entries.Single();

                    objContext.Refresh(RefreshMode.ClientWins, entry.Entity);
                    _dbContext.SaveChanges();


                }

            }


            //update the customer on result

            return false;

        }

        public ActionResult SignUpRegistration()
        {
            return View("SignUpRegistration");
        }

        private bool CheckIfUserExistsInSimpleMemberProvider(string userEmail)
        {
            try
            {
                UsersContext userContext = new UsersContext();
                var users = userContext.UserProfiles.ToList();

                foreach (var user in users)
                {
                    if (user.UserName == userEmail)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }

        private bool CheckIfCustomerEmailAlreadyExists(string email)
        {
            bool customerAlreadyExists = false;
            try
            {
                //split these two, check we ain't got anything in the dbs for that email already (Does it take care for us?)
                PortugalVillasContext _db = new PortugalVillasContext();


                var doesExistAlready = _db.Customers.Where(x => x.EmailAddress == email).ToList();

                if (doesExistAlready.Count > 0)
                {
                    customerAlreadyExists = true;
                }

                return customerAlreadyExists;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }



        [HttpPost]
        public ActionResult SignUpRegistration(CustomerRegistration registration)
        {

            bool customerAlreadyExists = CheckIfCustomerEmailAlreadyExists(registration.EmailAddress);
            bool customerLogonAlreadyExists = CheckIfUserExistsInSimpleMemberProvider(registration.EmailAddress);

            try
            {


                //customerLogonAlreadyExists = 


                //create customer if that email's not in use:               
                if (!customerAlreadyExists && !customerLogonAlreadyExists)
                {
                    Customer theCustomer = Customer.MapCustomerRegistrationToNewCustomer(registration);

                    FinalBookingDetailGatheringController customerController =
                        new FinalBookingDetailGatheringController();

                    var CustomerAdded = customerController.CreateCustomerAsPartOfSignUp(theCustomer);


                    string UserName = theCustomer.EmailAddress;
                    string Password = registration.Password;

                    //create the ASP.NET account
                    WebSecurity.CreateUserAndAccount(UserName, Password);

                    //make user a 'normal user'
                    if (!Roles.GetRolesForUser(UserName).Contains("User"))
                        Roles.AddUsersToRole(new[] { UserName }, "User");

                    //login and get newly created user
                    WebSecurity.Login(UserName, Password);

                    Session["prc_customer"] = theCustomer;

                    return View("SignUpSuccessful");
                    //log in with the new details


                }
                else
                {
                    return View("AlreadyExists");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong when trying to create a new user");
            }

        }


        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection, string returnUrl)
        {
            try
            {
                var lastPage = Request.UrlReferrer.ToString();
                string UserName = collection["Username"];
                string Password = collection["Password"];


                if (WebSecurity.Login(UserName, Password, persistCookie: false))
                {

                    GetCustomerForLoggedInCustomerAndStoreInSession(UserName);
                    GetCustomerBankDetailForLoggedInCustomerAndStoreInSession(UserName);
                    return RedirectToLocal(returnUrl);
                }

            }
            catch (ArgumentException ex)
            {

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View("LoginError");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View("LoginError");
        }

        //
        // POST: /Account/LogOff


        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginShort(FormCollection collection, string returnUrl)
        {
            try
            {
                var lastPage = Request.UrlReferrer.ToString();
                string UserName = collection["Username"];
                string Password = collection["Password"];

                int rememberMePost = Convert.ToInt32(collection["RememberMe"]);

                bool RememberMe = (rememberMePost == 1) ? true : false;

                if (WebSecurity.Login(UserName, Password, persistCookie: RememberMe))
                {

                    GetCustomerForLoggedInCustomerAndStoreInSession(UserName);
                    GetCustomerBankDetailForLoggedInCustomerAndStoreInSession(UserName);
                    Response.Redirect(lastPage);
                }

            }
            catch (ArgumentException ex)
            {

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View("LoginError");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View("LoginError");
        }



        [Authorize]
        public bool GetCustomerBankDetailForLoggedInCustomerAndStoreInSession(string email)
        {
            Customer customer = null;
            try
            {
                PortugalVillasContext _db = new PortugalVillasContext();

                if (Session["prc_customer"] == null)
                { customer = GetCustomerForLoggedInCustomerAndStoreInSession(email); }

                customer = (Customer)Session["prc_customer"];

                var customerBankDetail =
                    _db.CustomerBankDetails.Where(x => x.CustomerID == customer.CustomerID).FirstOrDefault();

                Session["prc_customerBankingDetail"] = (CustomerBankDetail)customerBankDetail;
            }
            catch (Exception exception)
            {

                throw exception;
            }

            return true;
        }


        public Customer GetCustomerForLoggedInCustomerAndStoreInSession(string email)
        {
            Customer customer = null;

            try
            {
                PortugalVillasContext _db = new PortugalVillasContext();

                customer =
                    _db.Customers.Where(x => x.EmailAddress.Trim().ToLower() == email.Trim().ToLower()).FirstOrDefault();

                Session["prc_customer"] = customer;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return (Customer)customer;
        }






        public void LogOff()
        {
            WebSecurity.Logout();

            Session["prc_customer"] = null;
            Session["prc_customerBankingDetail"] = null;


            Response.Redirect(Request.UrlReferrer.ToString());
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage



        /// <summary>
        /// Allows someone to update their details
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ManageCustomer()
        {
            PortugalVillasContext _dbContext = new PortugalVillasContext();


            //get the customer from the db according to who's logged in
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            //Show the customer to the suer
            Customer theCustomer = Customer.GetCustomerByEmailAddress(user);
            //update the customer
            Session["prc_previouscustomer"] = theCustomer;

            ViewBag.Title = "Manage Portugal Holiday Rentals Customer";
            return View(theCustomer);



        }


        [HttpPost]
        //  [Authorize(Roles = "Administrator, User")]
        public ActionResult ManageCustomer(Customer customer)
        {


            Customer previousCustomer = (Customer)Session["prc_previouscustomer"];
            customer.CustomerID = previousCustomer.CustomerID;


            if ((!CheckIfCustomerEmailAlreadyExists(customer.EmailAddress) &&
                !CheckIfUserExistsInSimpleMemberProvider(customer.EmailAddress)) || (previousCustomer.EmailAddress.ToLower().Trim() == customer.EmailAddress.ToLower().Trim()))
            {

                PortugalVillasContext _dbContext = new PortugalVillasContext();
                UsersContext _usersContext = new UsersContext();

                //get the old customer detailf from the session



                //update simplemembership provider too
                var user =
                    _usersContext.UserProfiles.Where(
                        x => x.UserName.ToLower().Trim() == previousCustomer.EmailAddress.ToLower().Trim())
                        .FirstOrDefault();

                user.UserName = customer.EmailAddress;

                //update customer and user
                try
                {
                    if (ModelState.IsValid)
                    {
                        _dbContext.Entry(customer).State = EntityState.Modified;
                        var objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;

                        var refreshableObjects = (from entry in objContext.ObjectStateManager.GetObjectStateEntries(
                            EntityState.Added
                            | EntityState.Deleted
                            | EntityState.Modified
                            | EntityState.Unchanged)
                                                  where entry.EntityKey != null
                                                  select entry.Entity);

                        objContext.Refresh(RefreshMode.ClientWins, refreshableObjects);

                        //if it works, do the update for the userContext, else, don't as it failed
                        if (objContext.SaveChanges() > 0)
                        {
                            _usersContext.Entry(user).State = EntityState.Modified;
                            _usersContext.SaveChanges();

                            WebSecurity.Logout();

                            Session["prc_customer"] = customer;
                            //update the customer
                            return View("CustomerUpdateSuccess");
                        }

                    }
                }

                catch
            (DbUpdateConcurrencyException ex)
                {
                    var objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
                    var entry = ex.Entries.Single();

                    objContext.Refresh(RefreshMode.ClientWins, entry.Entity);
                    _dbContext.SaveChanges();


                }

            }


            //update the customer on result
            ViewBag.Title = "Manage Portugal Holiday Rentals Customer";
            return View("CustomerUpdateFailed");


        }


        public ActionResult ManageCustomerPassword()
        {
            PortugalVillasContext _dbContext = new PortugalVillasContext();
            UsersContext _usersContext = new UsersContext();

            //get the customer from the db according to who's logged in

            //Show the customer to the suer

            //update the customer

            //update the SIMPLEMEMBERSHIPPROVDER CORRESPONDING RECORD!! (if they change the email!!)

            //update the 
            return View();

        }




        [Authorize(Roles = "Administrator, User")]
        public ActionResult ManageCustomerBankDetail()
        {


            var customer =
                Customer.GetCustomerByEmailAddress(
                    System.Web.HttpContext.Current.User.Identity.Name.ToString());

            var bankDetail = CustomerBankDetail.GetCustomerBankDetailFromCustomer(customer);

            if (bankDetail == null)
            {
                return View("AddCustomerBankDetail");

            }

            return View(bankDetail);
        }



        [Authorize(Roles = "Administrator, User")]
        public ActionResult AddCustomerBankDetail()
        {

            var customer =
                Customer.GetCustomerByEmailAddress(
                    System.Web.HttpContext.Current.User.Identity.Name.ToString());


            return View("AddCustomerBankDetail");
        }


        [HttpPost]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult AddCustomerBankDetail(CustomerBankDetail aCustomerBankDetail)
        {
            try
            {
                //customer must be logged in to do this - get logged in customer and assign the ID to the bank detail
                Customer customer = GetCustomerForLoggedInCustomerAndStoreInSession(HttpContext.User.Identity.Name);
                aCustomerBankDetail.CustomerID = customer.CustomerID;

                PortugalVillasContext _dbContext = new PortugalVillasContext();


                if (ModelState.IsValid)
                {
                    _dbContext.CustomerBankDetails.Add(aCustomerBankDetail);

                    if (_dbContext.SaveChanges() > 0) ;
                    {
                        return View("CustomerUpdateSuccess");

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return View("CustomerUpdateFailed");
        }


        [HttpPost]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult ManageCustomerBankDetail(CustomerBankDetail aCustomerBankDetail)
        {
            try
            {
                PortugalVillasContext _dbContext = new PortugalVillasContext();
                Customer customer = GetCustomerForLoggedInCustomerAndStoreInSession(HttpContext.User.Identity.Name);
                aCustomerBankDetail.CustomerID = customer.CustomerID;
                _dbContext.Entry(aCustomerBankDetail).State = EntityState.Modified;

                var objContext = ((IObjectContextAdapter)_dbContext).ObjectContext;

                var refreshableObjects = (from entry in objContext.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Added
                    | EntityState.Deleted
                    | EntityState.Modified
                    | EntityState.Unchanged)
                                          where entry.EntityKey != null
                                          select entry.Entity);

                objContext.Refresh(RefreshMode.ClientWins, refreshableObjects);


                if (ModelState.IsValid)
                {

                    if (objContext.SaveChanges() > 0) ;
                    {
                        return View("CustomerUpdateSuccess");

                    }

                }


            }
            catch (Exception exception)
            {

                throw exception;
            }


            return View("CustomerUpdateFailed");
        }





        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion



        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(FormCollection userForm)
        {

            string resetURL = Url.Action("ResetPassword", "Account", new { }, Request.Url.Scheme);


            
            var userstring = userForm["Email"];
            //try get the username
            var user = userContext.UserProfiles.Where(x => x.UserName.ToLower().Trim().Equals(userstring.ToLower().Trim())).FirstOrDefault();

            if (user != null)
            {
                //if exists, generate the token
                var token = WebSecurity.GeneratePasswordResetToken(user.UserName);

                //generate an email with the link
                var emailService = new EmailController();
                var email = emailService.CreateForgottenPasswordEmailTemplate(resetURL, user.UserName, token);

                var emailSent = email.SendEmail();

                ViewBag.Message = "A password reset has been sent, please check your email and click the link enclosed.";
                return View("PasswordResetSent");

            }


            ViewBag.Message = "Sorry we couldn't find that email address in the system. Please double check the email address you entered.";
            return View("PasswordResetSent");

        }


        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(FormCollection passwordForm)
        {
            
            //make sure you're logged out if you're logged in
            WebSecurity.Logout();

            var password = passwordForm["password"];
            var passwordToken = passwordForm["passwordToken"];

            if (WebSecurity.ResetPassword(passwordToken, password))
            {
                WebSecurity.Logout();
                return View("PasswordChanged");
            }
            else
            {
                return View("PasswordChangedFailed");
            }

           
        }
    }
}
