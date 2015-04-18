using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class CommentReply
    {
        public long CommentReplyID { get; set; }
        public long CommentID { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public Nullable<bool> Approved { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
