using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using UfXtract;
using UfXtract.Utilities;

namespace UfXtract.UnitTests
{

    [TestFixture]
    public class test_PhoneNumber
    {


        [Test]
        public void Test_PhoneNumber()
        {
            PhoneNumber phoneNumber = new PhoneNumber();

            PhoneNumberCanonicalised("01273 715100", "01273715100");
            PhoneNumberCanonicalised("+44 1273 715100", "+441273715100");
            PhoneNumberCanonicalised("800-555-1212", "8005551212");
            PhoneNumberCanonicalised("800.555.1212", "8005551212");
            PhoneNumberCanonicalised("(800) 555-1212", "8005551212");
            PhoneNumberCanonicalised("800-555-1212x1234", "80055512121234");
            PhoneNumberCanonicalised("800-555-1212 ext. 1234", "80055512121234");
            PhoneNumberCanonicalised("work 1-(800) 555.1212 #1234", "180055512121234");

        }

        public void PhoneNumberCanonicalised(string input, string output)
        {
            PhoneNumber phoneNumber = new PhoneNumber( input );
            Assert.That(phoneNumber.Canonicalised, Is.EqualTo(output));
        }

     



    }
}
