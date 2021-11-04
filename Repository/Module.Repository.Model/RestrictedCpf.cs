using Module.Repository.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Repository.Model
{
    public class RestrictedCpf : BaseIdentityModel<Guid>
    {
        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}