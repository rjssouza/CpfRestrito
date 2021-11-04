using Module.Repository.Model;
using Module.Service.Interface;
using Module.Service.Validation.Base;
using Module.Service.Validation.Interface;
using Module.Util.Extensao;
using System;
using System.Text.RegularExpressions;

namespace Module.Service.Validation
{
    public class RestrictedCpfValidation : BaseCrudValidation<RestrictedCpf>, IRestrictedCpfValidation
    {
        public IRestrictedCpfService RestrictedCpfService { get; set; }

        public override void ValidateDeletion(RestrictedCpf model)
        {
            base.ValidateDeletion(model);

            RestrictedCpf_RestrictedCpfMustExists(model);
            OnValidated();

            Cpf_ValidateCpfFormatting(model);
            OnValidated();
        }

        public override void ValidateInsert(RestrictedCpf model)
        {
            base.ValidateInsert(model);
            Cpf_ValidateCpfFormatting(model);
            Cpf_ValidateCpf(model);
            OnValidated();

            Cpf_CpfCannotBeDuplicate(model);
            OnValidated();
        }

        public void ValidateSearch(RestrictedCpf restrictedCpf)
        {
            RestrictedCpf_RestrictedCpfMustExists(restrictedCpf);
            OnValidated();

            Cpf_ValidateCpfFormatting(restrictedCpf);
            OnValidated();
        }

        public override void ValidateUpdate(RestrictedCpf model)
        {
            base.ValidateUpdate(model);
            Cpf_ValidateCpfFormatting(model);
            Cpf_ValidateCpf(model);
            OnValidated();

            Cpf_CpfCannotBeDuplicate(model);
            OnValidated();
        }

        private void Cpf_CpfCannotBeDuplicate(RestrictedCpf restrictedCpf)
        {
            var message = "Este cpf já existe";
            try
            {
                var existingCpf = RestrictedCpfService.GetByCpf(restrictedCpf.Cpf);

                if (existingCpf != null)
                    AddError("ExistsCpfException", message);
            }
            catch (Exception)
            {
                _summary.Errors.Clear();
            }
            finally
            {
            }
        }

        private void Cpf_ValidateCpf(RestrictedCpf restrictedCpf)
        {
            var message = "Cpf inválido";

            if (!restrictedCpf.Cpf.IsCpf())
                AddError("InvalidCpfException", message);
        }

        private void Cpf_ValidateCpfFormatting(RestrictedCpf restrictedCpf)
        {
            var message = "Cpf mal formatado";
            var regex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");

            if (!regex.IsMatch(restrictedCpf.Cpf))
                AddError("InvalidCpfException", message);
        }

        private void RestrictedCpf_RestrictedCpfMustExists(RestrictedCpf model)
        {
            var message = "Cpf não existe";

            if (model == null)
                AddError("NotFoundCpfException", message);
        }
    }
}