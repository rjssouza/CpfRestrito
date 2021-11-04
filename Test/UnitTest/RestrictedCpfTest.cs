using Autofac;
using Module.Dto;
using Module.Repository.Interface.Base;
using Module.Repository.Model;
using Module.Service.Interface;
using NUnit.Framework;
using STI.Domain.Test.Constants;
using System;
using System.Collections.Generic;
using UnitTest.Base;

namespace UnitTest
{
    [TestFixture]
    public class RestrictedCpfTest : BaseTest
    {
        private const string Cpf_CpfCannotBeDuplicate = "ExistsCpfException - Este cpf já existe";
        private const string Cpf_ValidateCpf = "InvalidCpfException - Cpf inválido";
        private const string RestrictedCpf_RestrictedCpfMustExists = "NotFoundCpfException - Cpf não existe";

        [TestCase(TestVal.STR_LENGTH_10, TestVal.VALID_CPF, "03/11/2021", Cpf_CpfCannotBeDuplicate, TestName = "InserirCpf_CpfCannotBeDuplicate")]
        [TestCase(TestVal.STR_LENGTH_10, TestVal.INVALID_CPF, "03/11/2021", Cpf_ValidateCpf, TestName = "InserirCpf_ValidateCpf")]
        [TestCase(TestVal.STR_LENGTH_10, TestVal.VALID_CPF_2, "03/11/2021", TestVal.STR_EMPTY, TestName = "InserirCpf")]
        public void TestInsertion(string name, string cpf, DateTime createdAt, string expectedMessage)
        {
            var restrictedCpfServce = Container.Resolve<IRestrictedCpfService>();
            var restrictedCpf = new RestrictedCpfDto()
            {
                Name = name,
                Cpf = cpf,
                CreatedAt = createdAt
            };

            void action() => restrictedCpfServce.Insert(restrictedCpf);

            AssertValidationExceptionMessage(action, expectedMessage);
        }


        [TestCase(TestVal.VALID_CPF_2, RestrictedCpf_RestrictedCpfMustExists, TestName = "SearchCpf_RestrictedCpfMustExists")]
        [TestCase(TestVal.VALID_CPF, TestVal.STR_EMPTY, TestName = "SearchCpf")]
        public void TestSearch(string cpf, string expectedMessage)
        {
            var restrictedCpfServce = Container.Resolve<IRestrictedCpfService>();

            void action() => restrictedCpfServce.GetByCpf(cpf);

            AssertValidationExceptionMessage(action, expectedMessage);
        }

        protected override void MockRepository()
        {
            var restrictedCpfList = new List<RestrictedCpf>()
            {
                new RestrictedCpf() { Cpf  = TestVal.VALID_CPF, CreatedAt  =DateTime.Now, Name = TestVal.STR_LENGTH_10, Id = TestVal.GUID_TEST_1 }
            };

            TestMock.Mock<IBaseCrudRepository<RestrictedCpf>>()
                .Setup(t => t.GetAll())
                .Returns(restrictedCpfList);
        }
    }
}