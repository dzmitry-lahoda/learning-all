using System;
using System.Collections.Generic;

namespace Itransition.Training.Data
{
    /// <summary>
    /// Busines persistent object Article
    /// </summary>
    public class Article
    {
        private int id;

        private String athour;

        private String title;

        private String body;

        private IList<Tag> tags = new List<Tag>();

        private DateTime lastupdate;

        private long version;

        /// <summary>
        /// Required by NHibernate.
        /// </summary>
        protected Article()
        {
        }

        /// <summary>
        /// Creates new article.
        /// </summary>
        /// <param name="athour">Athour name of article.</param>
        /// <param name="title">Article's name.</param>
        /// <param name="body">Formatted article's text.</param>
        /// <param name="tags">Corresponding tags.</param>
        /// <param name="lastupdate">DateTime of last update.</param>
        public Article(String athour, String title, String body,IList<Tag> tags, DateTime lastupdate)
        {
            this.athour = athour;
            this.title = title;
            this.body = body;
            this.tags = tags;
            this.lastupdate = lastupdate;
        }

        /// <summary>
        /// Gets article Id.
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
        /// Gets and sets article athour.
        /// </summary>
        public virtual String Athour
        {
            get
            {
                return athour;
            }
            set
            {
                athour = value;
            }
        }

        /// <summary>
        /// Gets and sets article title.
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
        /// Gets article title.
        /// </summary>
        public virtual IList<Tag> Tags
        {
            get
            {
                return tags;
            }
            protected set
            {
                tags = value;
            }
        }

        /// <summary>
        /// Gets and sets article body.
        /// </summary>
        public virtual String Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }
        }

        /// <summary>
        /// Gets and sets DateTime of last update .
        /// </summary>
        public virtual DateTime LastUpdate
        {
            get
            {
                return lastupdate;
            }
            set
            {
                lastupdate = value;
            }
        }

        /// <summary>
        /// Gets version of article(optimistic-lock property).
        /// </summary>
        public virtual long Version
        {
            get
            {
                return version;
            }
            protected set
            {
                version = value;
            }
        }

    }
}