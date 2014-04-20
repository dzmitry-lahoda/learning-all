using System;
using System.Collections.Generic;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Expression;

namespace Itransition.Training.Data
{

    //TODO:Prevent execution of unnecessary queries while using Editor
    /// <summary>
    /// Manipulates data in database.
    /// </summary>
    public static class ArticleEditor
    {

        /// <summary>
        /// Adds new article to database.
        /// </summary>
        /// <param name="article"></param>
        /// <returns>False if article with such athour and title exists.</returns>
        public static bool AddArticle(Article article)
        {
            ISession session = NHibernateHelper.GetSession();
            using (session)
            {
                ITransaction transaction = session.BeginTransaction();

                for (int i = 0; i < article.Tags.Count; i++)
                {
                    IList<Tag> list = session.CreateCriteria(typeof(Tag)).
                        Add(Expression.Eq("Title", article.Tags[i].Title)).
                        List<Tag>();
                    if (list.Count == 0)
                    {
                        session.SaveOrUpdate(article.Tags[i]);
                    }
                    else
                    {
                        article.Tags[i] = list[0];

                        /*article.Tags.RemoveAt(i);
                        list[0].Articles.Add(article);
                        session.SaveOrUpdate(article.Tags[i]);*/
                    }
                }
                if (!ContainsArticleWithAthourAndTitle(article))
                {
                    session.SaveOrUpdate(article);
                    transaction.Commit();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes from database article specified by athour and title.
        /// If such article does not exist, than do nothing.
        /// </summary>
        /// <param name="articleAthour"></param>
        /// <param name="articleTitle"></param>
        public static void DeleteArticleByAthourAndTitle(String articleAthour, String articleTitle)
        {
            ISession session = NHibernateHelper.GetSession();
            using (session)
            {
                ITransaction transaction = session.BeginTransaction();
                Article article = session.CreateCriteria(typeof(Article)).
                    Add(Expression.Eq("Athour", articleAthour)).
                    Add(Expression.Eq("Title", articleTitle)).
                    UniqueResult<Article>();
                session.Delete(article);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Updates article in database.
        /// </summary>
        /// <param name="article"></param>
        /// <returns>False if article's version is older than version in database.</returns>
        public static bool UpdateArticle(Article article)
        {
            ISession session = NHibernateHelper.GetSession();
            using (session)
            {
                ITransaction transaction = session.BeginTransaction();

                for (int i = 0; i < article.Tags.Count; i++)
                {
                    IList<Tag> list = session.CreateCriteria(typeof(Tag)).
                        Add(Expression.Eq("Title", article.Tags[i].Title)).
                        List<Tag>();
                    if (list.Count == 0)
                    {
                        session.SaveOrUpdate(article.Tags[i].Title);
                    }
                    else
                    {
                        article.Tags[i] = list[0];
                    }
                }
                if (article.Version >
                    session.CreateCriteria(typeof(Article)).
                    Add(Expression.Eq("Id", article.Id)).
                    UniqueResult<Article>().Version)
                {
                    session.SaveOrUpdate(article);
                    transaction.Commit();
                    return true;
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        private static IList<Article> GetArticlesWithTag(ISession session, String tagTitle)
        {
                return session.CreateCriteria(typeof(Tag)).
                    Add(Expression.Eq("Title", tagTitle)).
                    UniqueResult<Tag>().Articles;
        }

        /// <summary>
        /// Returns all articles associated with specified tag.
        /// </summary>
        /// <param name="tagTitle">Tag's title.</param>
        /// <returns></returns>
        public static IList<Article> GetArticlesWithTag(String tagTitle)
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
         
                Tag tag = session.CreateCriteria(typeof(Tag)).
                    Add(Expression.Eq("Title", tagTitle)).
                    UniqueResult<Tag>();
                List<Article> articles = new List<Article>();
                if (tag!=null)
                {
                    articles.AddRange(tag.Articles);
                }
                /*foreach (Article article in articles)
                {
                    foreach (Tag tag in article.Tags)
                    {

                    }
                }*/
                return articles;
            }

        }

        private static IList<Article> GetAllArticles(ISession session)
        {
            return session.CreateCriteria(typeof(Article)).List<Article>();
        }

        /// <summary>
        /// Returns all articles.
        /// </summary>
        /// <returns></returns>
        public static IList<Article> GetAllArticles()
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                return GetAllArticles(session);
            }
        }

        private static IList<Article> GetArticlesByAthour(ISession session, String articleAthour)
        {
            return session.CreateCriteria(typeof(Article)).
                Add(Expression.Eq("Athour", articleAthour)).
                List<Article>();
        }

        /// <summary>
        /// Returns all articles posted by specidfied athour.
        /// </summary>
        /// <param name="articleAthour">Athour name.</param>
        /// <returns></returns>
        public static IList<Article> GetArticlesByAthour(String articleAthour)
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                return GetArticlesByAthour(session, articleAthour);
            }
        }

        private static Article GetArticleWithAthourAndTitle(ISession session, String articleAthour, String articleTitle)
        {
            return session.CreateCriteria(typeof(Article)).
                Add(Expression.Eq("Athour", articleAthour)).
                Add(Expression.Eq("Title", articleTitle)).
                UniqueResult<Article>();
        }

        /// <summary>
        /// Returns article with specified athour and title; null if there is no such article.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static Article GetArticleWithAthourAndTitle(String articleAthour, String articleTitle)
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                return GetArticleWithAthourAndTitle(session, articleAthour, articleTitle);
            }
        }

        private static bool ContainsArticleWithAthourAndTitle(ISession session, Article article)
        {
            return session.CreateCriteria(typeof(Article)).
                Add(Expression.Eq("Athour", article.Athour)).
                Add(Expression.Eq("Title", article.Title)).
                UniqueResult<Article>() != null;
        }

        /// <summary>
        /// Returns true if database contains article with specified athour and title; otherwise false.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static bool ContainsArticleWithAthourAndTitle(Article article)
        {
            using (ISession session = NHibernateHelper.GetSession())
            {
                return ContainsArticleWithAthourAndTitle(session, article);
            }
        }

    }
}
