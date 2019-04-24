using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SevenDB;
using SevenLib;

namespace UnitTestSeven
{
    [TestClass]
    public class UnitTest
    {
        private List<Author> authors;
        private List<Book> books;
        private List<Copy> copies;
        private List<Loan> loans;
        private List<Member> members;

        private RepositoryAuthor ra;
        private RepositoryBook rb;
        private RepositoryCopy rc;
        private RepositoryLoan rl;
        private RepositoryMember rm;

        [TestInitialize]
        public void Initialize()
        {
            authors = new List<Author>();
            books = new List<Book>();
            copies = new List<Copy>();
            loans = new List<Loan>();
            members = new List<Member>();

            ra = new RepositoryAuthor();
            rb = new RepositoryBook();
            rc = new RepositoryCopy();
            rl = new RepositoryLoan();
            rm = new RepositoryMember();
        }


        [TestMethod]
        public void AddData()
        {
            Author a1 = new Author("last1", "first1", DateTime.Today, SevenLib.Helpers.Nationality.FR, DateTime.Today);
            Author a2 = new Author("last2", "first2", DateTime.Today, SevenLib.Helpers.Nationality.DE, DateTime.Today);
            Author a3 = new Author("last3", "first3", DateTime.Today, SevenLib.Helpers.Nationality.VE, DateTime.Today);
            authors.Add(a1);
            authors.Add(a2);
            authors.Add(a3);

            Member m1 = new Member("last1", "first1", "mail1@ddd.com", "gmorzKGPZJERTGMLZQJRGML25454###", true, "064341563341354555");
            Member m1b = new Member("last1", "first1", "mail2@laposte.fr", "gmorzKGPZJERsTGMLZQJRGMgdsgL25454###", false);
            Member m2 = new Member("last2", "first2", "mail2@marijanne.co", "gmorzKGPZJERTGMLZgsfdgQJRGML25454###");
            members.Add(m1);
            members.Add(m1b);
            members.Add(m2);

            foreach(Author a in authors)
            {
                Assert.IsTrue(ra.AddAuthor(a), "Assert on AddAuhtor");
            }

            foreach(Member m in members)
            {
                Assert.IsTrue(rm.AddMember(m), "Assert on AddMember");
            }
        }

        [TestMethod]
        public void GetData()
        {
            authors = ra.GetAuthors().ToList<Author>();
            members = rm.GetMembers().ToList<Member>();

            foreach (Author a in authors)
            {
                Assert.IsNotNull(a, "Assert on authors");
            }

            foreach (Member m in members)
            {
                Assert.IsNotNull(m, "Assert on members");
            }
        }

        [TestMethod]
        public void DeleteDate()
        {
            GetData();

            foreach (Author a in authors)
            {
                Assert.IsTrue(ra.DeleteAuthorByID((int)a.ID), "Assert on DeleteAuthor");
            }

            foreach(Member m in members)
            {
                Assert.IsTrue(rm.DeleteMemberByID((int)m.ID), "Assert on DeleteMember");
            }
        }

        [TestMethod]
        public void HashPassword()
       {
            String clearpwd = "a random password with whitespaces";

            Assert.AreNotEqual(clearpwd, SevenLib.Helpers.HashHelper.HashString(clearpwd));
        }
    }
}
