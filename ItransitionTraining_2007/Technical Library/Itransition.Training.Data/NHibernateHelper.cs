using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Itransition.Training.Data
{
    public static class NHibernateHelper
    {
        private static readonly ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

        public static ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
