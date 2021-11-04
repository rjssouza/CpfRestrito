using AutoMapper;
using Module.IoC.Mapper.Configuracoes;
using System;

namespace Module.IoC.Mapper
{
    /// <summary>
    /// Mapper configuration
    /// </summary>
    public static class MapperRegistration
    {
        /// <summary>
        /// Método chamada na inicialização para configurar as conversões
        /// </summary>
        /// <param name="mapperConfigExpression"></param>
        public static void ConfigureMapper(IMapperConfigurationExpression mapperConfigExpression)
        {
            if (mapperConfigExpression is null)
            {
                throw new ArgumentNullException(nameof(mapperConfigExpression));
            }

            mapperConfigExpression.AddProfile(new RestrictedCpfProfile());
        }
    }
}