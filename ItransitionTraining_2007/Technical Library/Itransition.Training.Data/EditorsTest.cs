using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NUnit.Framework;

namespace Itransition.Training.Data
{
    [TestFixture]
    public class EditorsTest
    {

        private TextWriter output = Console.Out;

        private readonly Tag[] tags ={new Tag("pi"),new Tag("e"),new Tag("sin"),
        new Tag("numbers"),new Tag("e"),new Tag("root"),
        new Tag("int"), new Tag("long"), new Tag("decimal")};
        private readonly Tag[] repeatedTags ={ new Tag("cos"), new Tag("cos"), new Tag("tan") };
        private readonly Tag[] emptyTags ={ };
        private readonly Tag addTag = new Tag("modul");
        private readonly Tag deleteTag = new Tag("topology");
        private readonly Tag tag = new Tag("negative");

        private readonly int n = 10;

        private readonly Random random = new Random();

        private readonly String[] athours = { "d.lagoda", "marina s", "vasia pupkin", "petia", "pavel", "lena" };
        private readonly String[] titles = { "Math", "Geometry", "Substraction and addition", "Sin and Cos", "+/-*=", "Equality" };

        private Article CreateArticle()
        {
            int athoursIndex = random.Next(0, athours.Length - 1);
            int titlesIndex = random.Next(0, titles.Length - 1);
            int tagEndIndex = random.Next(0, tags.Length - 1);
            int tagBeginIndex= random.Next(0, tagEndIndex);
            return new Article(athours[athoursIndex], 
                titles[titlesIndex],
                "body "+random.Next(),
                tags,
                DateTime.Now); 
        }

        [SetUp]
        public void  SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test, Category("TagEditor Tests")]
        public void AddTagWithTitle()
        {
            TagEditor.AddTagWithTitle(addTag.Title);
        }

        [Test, Category("TagEditor Tests")]
        public void DeleteTagWithTitle()
        {
            TagEditor.DeleteTagWithTitle(deleteTag.Title);
        }

        [Test, Category("TagEditor Tests")]
        public void GetAllTags()
        {
            IList<Tag> tags = TagEditor.GetAllTags();
            foreach (Tag tag in tags)
            {
                output.WriteLine(tag.Title);
            }
        }

        [Test, Category("TagEditor Tests")]
        public void ContainsTagWithTitle()
        {
            AddTagWithTitle();
            Assert.IsTrue(TagEditor.ContainsTagWithTitle(addTag.Title));
        }

        [Test, Category("TagEditor Tests")]
        public void GetTagsWithTitleLikeThis()
        {
            foreach (Tag tag in TagEditor.GetTagsWithTitleLikeThis("qw"))
            {
                output.WriteLine("tag like " + tag.Title);
            }
        }

        [Test, Category("TagEditor Tests")]
        public void GetTagsWithNumberOfAssociatedArticles()
        {
            TagEditor.GetTagsWithNumberOfAssociatedArticles();
        }


        [Test, Category("ArticleEditor Tests")]
        public void AddArticle()
        {
            IList<Tag> tags = new List<Tag>();
            tags.Add(new Tag("teta"));
            Article article = new Article("rtyrgfdhgtyrtyrt", "worker", "hello world", tags, DateTime.Now);
            ArticleEditor.AddArticle(article);

            article = new Article("fghf", "worker", "hello heaven", tags, DateTime.Now);
            ArticleEditor.AddArticle(article);
        }

        [Test, Category("ArticleEditor Tests")]
        public void DeleteArticleByAthourAndTitle()
        {
            ArticleEditor.DeleteArticleByAthourAndTitle("rtyrgfdhgtyrtyrt", "worker");
        }

        [Test, Category("ArticleEditor Tests")]
        public void GetArticle()
        {
        }
        
        [Test, Category("ArticleEditor Tests")]
        public void GetArticlesWithTag()
        {
            IList<Article> articles = ArticleEditor.GetArticlesWithTag("qwe");
            foreach (Article article in articles)
            {
                foreach (Tag tag in article.Tags)
                {

                }
            }
        }

        [Test, Category("ArticleEditor Tests")]
        public void GetAllArticles()
        {
            IList<Article> articles = ArticleEditor.GetAllArticles();
            foreach (Article article in articles)
            {
                foreach (Tag tag in article.Tags)
                {
                    
                }
            }
        }



    }
}
