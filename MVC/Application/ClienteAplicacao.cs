﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Context;
using MVC.Models;

namespace MVC.Aplicacao
{
    public class ClienteAplicacao
    {
        private BANCOContext _contexto;

        public ClienteAplicacao(BANCOContext contexto)
        {
            _contexto = contexto;
        }

        public string InserirCliente(Cliente cli)
        {
            try
            {
                if (cli != null)
                {
                    var clienteExiste = GetCliByID(cli.CodCli);

                    if (clienteExiste == null)
                    {
                        _contexto.Add(cli);
                        _contexto.SaveChanges();

                        return "Cliente cadastrado com sucesso!";
                    }
                    else
                    {
                        return "Cliente já cadastrado na base de dados.";
                    }
                }
                else
                {
                    return "Cliente inválido!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string AtualizarCliente(Cliente cli)
        {
            try
            {
                if (cli != null)
                {
                    _contexto.Update(cli);
                    _contexto.SaveChanges();

                    return "Cliente alterado com sucesso!";
                }
                else
                {
                    return "Cliente inválido!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Cliente GetCliByID(int codCli)
        {
            Cliente primeiroCliente = new Cliente();

            try
            {
                if (codCli == 0)
                {
                    return null;
                }

                var cli = _contexto.Clientes.Where(x => x.CodCli == codCli).ToList();
                primeiroCliente = cli.FirstOrDefault();

                if (primeiroCliente != null)
                {
                    return primeiroCliente;
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

        public List<Cliente> GetAllClients()
        {
            List<Cliente> listaDClientes = new List<Cliente>();
            try
            {

                listaDClientes = _contexto.Clientes.ToList(); //_contexto.Clientes.Select(x => x).ToList();

                if (listaDClientes != null)
                {
                    return listaDClientes;
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

        public string DeleteClientByCod(int codCli)
        {
            try
            {
                if (codCli == 0)
                {
                    return "Cliente inválido! Por favor tente novamente.";
                }
                else
                {
                    var cli = GetCliByID(codCli);

                    if (cli != null)
                    {
                        _contexto.Clientes.Remove(cli);
                        _contexto.SaveChanges();

                        return "Cliente " + cli.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
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
