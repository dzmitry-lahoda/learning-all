using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;

namespace Itransition.Training.Data
{
    public static class TagEditor
    {
        public static void AddTagWithTitle(ISession session, String tagTitle)
        {
            using (session)
            {
                if (session.CreateCriteria(typeof(Tag)).Add(Expression.Eq("Title", tagTitle)).List<Tag>().Count == 0)
                {
                    ITransaction transaction = session.BeginTransaction();
                    session.SaveOrUpdate(new Tag(tagTitle));
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Add tag with specified title to database. 
        /// If  such tag exists, than do nothing.
        /// </summary>
        /// <param name="tagTitle"></param>
        public static void AddTagWithTitle(String tagTitle)
        {
            ISession session = NHibernateHelper.GetSession();
            AddTagWithTitle(session, tagTitle);
        }

        private static bool ContainsTagWithTitle(ISession session, String tagTitle)
        {
            using (session)
            {
                IList<Tag> list = session.CreateCriteria(typeof(Tag)).Add(Expression.Eq("Title", tagTitle)).List<Tag>();
                return list.Count > 0;
            }
        }

        /// <summary>
        /// Returns true if database contains tag with specified title; 
        /// otherwise false.
        /// </summary>
        /// <param name="tagTitle"></param>
        /// <returns></returns>
        public static bool ContainsTagWithTitle(String tagTitle)
        {
            ISession session = NHibernateHelper.GetSession();
            return ContainsTagWithTitle(session, tagTitle);
        }

        private static void DeleteTagWithTitle(ISession session, String tagTitle)
        {
            using (session)
            {
                ITransaction transaction = session.BeginTransaction();
                session.Delete(
                     String.Format(@"from Tag as tag where tag.Title='{0}'", tagTitle)
                    );
                transaction.Commit();
            }
        }

        /// <summary>
        /// Deletes tag with specified title from database. 
        /// If such tag does not exist, than do nothing.
        /// </summary>
        /// <param name="tagTitle">Title of tag.</param>
        public static void DeleteTagWithTitle(String tagTitle)
        {
            ISession session = NHibernateHelper.GetSession();
            DeleteTagWithTitle(session, tagTitle);
        }

        private static IList<Tag> GetAllTags(ISession session)
        {
            using (session)
            {
                return session.CreateCriteria(typeof(Tag)).List<Tag>();
            }
        }

        /// <summary>
        /// Returns all tags from database.
        /// </summary>
        /// <returns></returns>
        public static IList<Tag> GetAllTags()
        {
            ISession session = NHibernateHelper.GetSession();
            return GetAllTags(session);
        }


        private static IList<Tag> GetTagsWithTitleLikeThis(ISession session, String tagTitle)
        {
            using (session)
            {
                return session.CreateCriteria(typeof(Tag)).
                    Add(Expression.Like("Title", tagTitle + "%")).
                    List<Tag>();
            }
        }

        /// <summary>
        /// Returns tags which titles begin with specified string.  
        /// </summary>
        /// <param name="tagTitle">Beginning of tag's title.</param>
        /// <returns></returns>
        public static IList<Tag> GetTagsWithTitleLikeThis(String tagTitle)
        {
            ISession session = NHibernateHelper.GetSession();
            return GetTagsWithTitleLikeThis(session, tagTitle);
        }

        private static IList<KeyValuePair<String, int>> GetTagsWithNumberOfAssociatedArticles(ISession session)
        {
            IList<KeyValuePair<String, int>> list = new List<KeyValuePair<String, int>>();;
            using (session)
            {
                IList<Tag> tags = session.CreateCriteria(typeof(Tag)).List<Tag>();
                foreach (Tag tag in tags)
                {
                    list.Add(new KeyValuePair<string, int>(tag.Title, tag.Articles.Count));
                }
            }
            return list;
        }

        /// <summary>
        /// Returns list of tags with number of associated articles.
        /// </summary>
        /// <returns></returns>
        public static IList<KeyValuePair<String, int>> GetTagsWithNumberOfAssociatedArticles()
        {
            ISession session = NHibernateHelper.GetSession();
            return GetTagsWithNumberOfAssociatedArticles(session);
        }

    }
}
