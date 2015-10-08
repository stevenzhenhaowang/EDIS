using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDISAngular.Models.BindingModels;
using EDISAngular.Models.ModelValidators;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace EDISTEST
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void RequiredIfBoolTrueValidationTest()
        {
            var client = new PersonClientCreationBindingModel
            {
                firstName = "name",
                lastName = "name",
                email = "mail@mail.com",
                contactPhone = "99999999",
                isGroupLeader = true
            };

            var context = new ValidationContext(client, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(client, context, validationResults, true);



            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void AdviserMustExistValidationTestInvalid()
        {
            var client = new PersonClientCreationBindingModel
            {
                firstName = "name",
                lastName = "name",
                email = "mail@mail.com",
                contactPhone = "99999999",
                isGroupLeader = true,
                newGroupAccountName = "newGroup",
                newGroupAdviserNumber = 1000,
                newGroupAlias = "alias"
            };

            var context = new ValidationContext(client, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(client, context, validationResults, true);



            Assert.AreEqual(false, isValid);

        }


        [TestMethod]
        public void AdviserMustExistValidationTestValid()
        {
            var client = new PersonClientCreationBindingModel
            {
                firstName = "name",
                lastName = "name",
                email = "mail@mail.com",
                contactPhone = "99999999",
                isGroupLeader = true,
                newGroupAccountName = "newGroup",
                newGroupAdviserNumber = 1000,
                newGroupAlias = "alias"
            };

            var context = new ValidationContext(client, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(client, context, validationResults, true);



            Assert.AreEqual(true, isValid);

        }


        [TestMethod]
        public void EmailMustBeUniqueValidationTestinvalid()
        {
            var client = new PersonClientCreationBindingModel
            {
                firstName = "name",
                lastName = "name",
                email = "henry.wingyu1111@gmail.com",
                contactPhone = "99999999",
                isGroupLeader = true,
                newGroupAccountName = "newGroup",
                newGroupAdviserNumber = 1000,
                newGroupAlias = "alias"
            };

            var context = new ValidationContext(client, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(client, context, validationResults, true);



            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void EmailMustBeUniqueValidationTestValid()
        {
            var client = new PersonClientCreationBindingModel
            {
                firstName = "name",
                lastName = "name",
                email = "fdasfsdfsa11@gmail.com",
                contactPhone = "99999999",
                isGroupLeader = true,
                newGroupAccountName = "newGroup",
                newGroupAdviserNumber = 1000,
                newGroupAlias = "alias"
            };

            var context = new ValidationContext(client, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(client, context, validationResults, true);

            Assert.AreEqual(true, isValid);
        }

    }
}
