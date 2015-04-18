using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Models
{

    public partial class Comment
    {
       public static List<Comment> GetComments(Property aProperty)
       {
           PortugalVillasContext _db = new PortugalVillasContext();

           List<Comment> theCommentsList = new List<Comment>();

           theCommentsList = _db.Comments.Where(x => x.PropertyID == aProperty.PropertyID)
               .Where(x => x.Approved == true)               
               .ToList();

           return theCommentsList;
       }



        public static List<Comment> GetRandomTopRatedComments()
        {
            try
            {
                PortugalVillasContext _db = new PortugalVillasContext();

                List<Comment> theCommentsList = new List<Comment>();

                return theCommentsList = _db.Comments.Where(x => x.StarRating > 2)
               .Where(x => x.Approved == true).Take(10)
               .ToList();

                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }



            throw new Exception();

        }

       public static int GetPropertyStarRating(Property theProperty)
        {
            try
            {
                var comments = Comment.GetComments(theProperty).ToList();
                var thisPropertysNumberOfComments = //call methods to pull back commments
                    comments.Count;
                var sumOfComments = 0;

                foreach (var comment in comments)
                {
                    sumOfComments += comment.StarRating ?? 0;
                }

                var propertyOverallRating = sumOfComments / thisPropertysNumberOfComments;


                return (int)propertyOverallRating;
            }
            catch (DivideByZeroException dbzException)
            {

                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }

}