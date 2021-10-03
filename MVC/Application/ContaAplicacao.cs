using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Context;
using MVC.Models;

namespace MVC.Aplicacao
{
    public class ContaAplicacao
    {
        private BANCOContext _contexto;

        public ContaAplicacao(BANCOContext contexto)
        {
            _contexto = contexto;
        }

        public string InserirConta(Contum conta)
        {
            try
            {
                if (conta != null)
                {
                    var contaExiste = GetContaByID(conta.CodConta);

                    if (contaExiste == null)
                    {
                        _contexto.Add(conta);
                        _contexto.SaveChanges();

                        return "Conta cadastrada com sucesso!";
                    }
                    else
                    {
                        return "Conta já cadastrada na base de dados.";
                    }
                }
                else
                {
                    return "Conta inválida!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string AtualizarConta(Contum conta)
        {
            try
            {
                if (conta != null)
                {
                    _contexto.Update(conta);
                    _contexto.SaveChanges();

                    return "Conta alterada com sucesso!";
                }
                else
                {
                    return "Conta inválida!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Contum GetContaByID(int codConta)
        {
            Contum primeiraConta = new();

            try
            {
                if (codConta == 0)
                {
                    return null;
                }

                var conta = _contexto.Conta.Where(x => x.CodConta == codConta).ToList();
                primeiraConta = conta.FirstOrDefault();

                if (primeiraConta != null)
                {
                    return primeiraConta;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Contum> GetAllContas()
        {
            List<Contum> listaDContas = new();
            try
            {

                listaDContas = _contexto.Conta.ToList();

                if (listaDContas != null)
                {
                    return listaDContas;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string DeleteContaByCod(int codConta)
        {
            try
            {
                if (codConta == 0)
                {
                    return "Conta inválido! Por favor tente novamente.";
                }
                else
                {
                    var conta = GetContaByID(codConta);

                    if (conta != null)
                    {
                        _contexto.Conta.Remove(conta);
                        _contexto.SaveChanges();

                        return "Conta " + conta.Agencia + " deletada com sucesso!";
                    }
                    else
                    {
                        return "Conta não cadastrada!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

    }
}
