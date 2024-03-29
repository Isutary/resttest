﻿using NUnit.Framework;
using RESTTest.Common.Generators;
using RESTTest.Common.Setup;
using CommonConstants = RESTTest.Common.Constants;

namespace RESTTest.Registration
{
    [SetUpFixture]
    public class RegistrationTestsFixture : HeaderSetupFixture
    {
        public RegistrationTestsFixture() : base(CommonConstants.Host.IdentityService) { }

        [OneTimeSetUp]
        public void SetUp()
        {
            PlayerGenerator.CreatePlayer(Constants.Data.RegisterAccount.TestUsername, Constants.Data.RegisterAccount.TestEmail);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            PlayerGenerator.DeleteAll();
        }
    }
}
