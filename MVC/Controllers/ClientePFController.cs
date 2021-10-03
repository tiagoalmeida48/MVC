using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;
using MVC.Aplicacao;

namespace MVC.Controllers
{
    public class ClientePFController : Controller
    {
        private readonly BANCOContext _context;

        public ClientePFController(BANCOContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            ClienteAplicacao cliente = new(_context);
            return View(cliente.GetAllClients());
            //var bANCOContext = _context.Clientes.Include(c => c.CodTipoCliNavigation);
            //return View(await bANCOContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.CodTipoCliNavigation)
                .FirstOrDefaultAsync(m => m.CodCli == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis, "CodTipoCli", "NomeTipoCli");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodCli,CodTipoCli,Nome,DataNascFund,RendaLucro,Sexo,Email,Endereco,Documento,TipoEmpresa")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis, "CodTipoCli", "NomeTipoCli", cliente.CodTipoCli);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis, "CodTipoCli", "NomeTipoCli", cliente.CodTipoCli);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodCli,CodTipoCli,Nome,DataNascFund,RendaLucro,Sexo,Email,Endereco,Documento,TipoEmpresa")] Cliente cliente)
        {
            if (id != cliente.CodCli)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.CodCli))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis, "CodTipoCli", "NomeTipoCli", cliente.CodTipoCli);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.CodTipoCliNavigation)
                .FirstOrDefaultAsync(m => m.CodCli == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.CodCli == id);
        }
    }
}
