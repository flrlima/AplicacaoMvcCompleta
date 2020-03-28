using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AmcDbContext context) : base(context){ }

        public async Task<Endereco> ObterEnderecoPorForncedor(Guid forncecedorId)
        {
            return await _amcDbContext.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(e => e.FornecedorId == forncecedorId);
        }
    }
}
