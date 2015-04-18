using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Comment
    {
        public Comment()
        {
            this.CommentReplies = new List<CommentReply>();
        }

        public long CommentID { get; set; }
        public long PropertyID { get; set; }
        public string Username { get; set; }
        public Nullable<int> StarRating { get; set; }
        public Nullable<System.DateTime> StartdateOfStay { get; set; }
        public Nullable<System.DateTime> EnddateOfStay { get; set; }
        public bool Approved { get; set; }
        public string Text { get; set; }
        public Nullable<System.DateTime> WhenCreated { get; set; }
        public virtual Property Property { get; set; }
        public virtual ICollection<CommentReply> CommentReplies { get; set; }
    }
}
