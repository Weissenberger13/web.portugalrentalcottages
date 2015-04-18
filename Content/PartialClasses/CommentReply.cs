using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootstrapVillas.Models;

namespace BootstrapVillas.Models
{
    public partial class CommentReply
    {
        public static List<CommentReply> GetCommentReplies()
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            List<CommentReply> theCommentReplies = new List<CommentReply>();

            theCommentReplies = _db.CommentReplies.Where(x => x.Approved == true).ToList();
            
            return theCommentReplies;
        }


    }
}