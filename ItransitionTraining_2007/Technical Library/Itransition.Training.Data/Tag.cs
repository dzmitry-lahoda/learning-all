using System;
using System.Collections.Generic;

namespace Itransition.Training.Data
{
    /// <summary>
    /// Business persistent object Tag
    /// </summary>
    public class Tag
    {
        private int id;

        private String title;

        private IList<Article> articles=new List<Article>();

        /// <summary>
        /// Required by NHibernate.
        /// </summary>
        protected Tag()
        {
        }

        /// <summary>
        /// Creates new tag with specified title.
        /// </summary>
        /// <param name="title"></param>
        public Tag(String title)
        {
            this.title = title;
        }

        /// <summary>
        /// Gets tag Id
        /// </summary>
        public virtual int Id
        {
            get
            {
                return id;
            }
            protected set
            {
                id = value;
            }
        }

        /// <summary>
        /// Gets and sets title.
        /// </summary>
        public virtual String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// Gets articles markered by this tag.
        /// </summary>
        public virtual IList<Article> Articles
        {
            get
            {
                return new List<Article>(articles).AsReadOnly();
            }
            protected set
            {
                articles = value;
            }
        }

    }
}