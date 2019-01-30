using System;
using System.Collections.Generic;
using System.Text;
using WIMSystem.Models.Contracts;

namespace WIMSystem.Models
{
    public class Comment : IComment
    {
        private string message;
        private IMember author;

        public Comment(string message, IMember author)
        {
            if(string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("Message", "Message cannot be null or empty!");
            }
            if (author == null)
            {
                throw new ArgumentNullException("Author", "Author cannot be null or empty!");
            }
            this.message = message;
            this.author = author;
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public IMember Author
        {
            get
            {
                return this.author;
            }
        }
    }
}
