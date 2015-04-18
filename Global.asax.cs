using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using BootstrapVillas.Content.AutomationClasses;
using BootstrapVillas.Content.Classes;
using System.Timers;
using BootstrapVillas.Interfaces;
using WebMatrix.WebData;
using BootstrapVillas.Models;
using System.Data.Entity;


namespace BootstrapVillas
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public void Session_OnStart()
        {
            DirectoryInfo ServerPath = new DirectoryInfo(Server.MapPath(@"~\"));
            Session["ServerPath"] = ServerPath;


            Session["defaultCurrencySymbol"] = WebConfigurationManager.AppSettings["defaultCurrencySymbol"];
            Session["defaultCurrency"] = WebConfigurationManager.AppSettings["defaultCurrency"];

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();


            var binder = new BootstrapVillas.CustomModelBinders.DateTimeUKModelBinder();
            ModelBinders.Binders.Add(typeof(DateTime), binder);
            ModelBinders.Binders.Add(typeof(DateTime?), binder);


            Database.SetInitializer<PortugalVillasContext>(null);
            Database.SetInitializer<UsersContext>(null);

            //membership provider
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("PortugalVillasContext", "UserProfile", "UserId", "UserName",
                    true);
            }

            //update currency
            try
            {
                var autoMate = new PRCAutomations();
                var ok = autoMate.UpdateCurrencyExchangeRates();

            }
            catch (Exception ex)
            {

                //ignore
            }

            //get a token for the translator every 500 seconds and store it in the viewbag
            Timer translateTimer = new Timer(600000);
            translateTimer.Elapsed += translateTimer_Elapsed;
            Application["TranslateToken"] = GetBingAPITranslationToken();
            translateTimer.Enabled = true;
            GC.KeepAlive(translateTimer);


            //update currency every hr  
            Timer currencyUpdateTimer = new Timer(3600000);
            currencyUpdateTimer.Elapsed += currencyUpdateTimer_Elapsed;
            currencyUpdateTimer.Enabled = true;
            GC.KeepAlive(currencyUpdateTimer);



            Application["defaultCurrencySymbol"] = WebConfigurationManager.AppSettings["defaultCurrencySymbol"];
            Application["defaultCurrency"] = WebConfigurationManager.AppSettings["defaultCurrency"];

        }


        /// <summary>
        /// Gets a new token from the Bing API while the program is active
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void translateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application["TranslateToken"] = GetBingAPITranslationToken();
        }


        void currencyUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var autoMate =
              new PRCAutomations();
            var ok = autoMate.UpdateCurrencyExchangeRates();

            if (ok.Equals(0))
            {

            }
            else
            { }

        }



        public string GetBingAPITranslationToken()
        {
            string token = "";
            AdmAccessToken admToken;


            AdmAuthentication admAuth = new AdmAuthentication("portugal_rental_cottages", "okyIIda04oNosmgTuhDaJeGQCkkzniuXTieU+2/6bjU=");
            //AdmAuthentication admAuth = new AdmAuthentication("portugal_rental_cottages2", "OJ95iUpcz46bfiCDr3HuPZ3cGpSVBtXOwIzIfD50sdA=");

            try
            {
                admToken = admAuth.GetAccessToken();

                token = "Bearer " + admToken.access_token;

            }

            catch (Exception ex)
            {
                //ex.InnerException.ToString();

                //the translator won't work but hey, fuck it. 
                admToken = new AdmAccessToken();
                return token;
            }


            return token;
        }


    }
}