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
    public class ContumController : Controller
    {
        private readonly BANCOContext _context;

        public ContumController(BANCOContext context)
        {
            _context = context;
        }

        // GET: Contum
        public async Task<IActionResult> Index()
        {
            ContaAplicacao conta = new(_context);
            return View(conta.GetAllContas());
        }

        // GET: Contum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contum = await _context.Conta
                .Include(c => c.CodCliNavigation)
                .Include(c => c.CodTipoContaNavigation)
                .FirstOrDefaultAsync(m => m.CodConta == id);
            if (contum == null)
            {
                return NotFound();
            }

            return View(contum);
        }

        // GET: Contum/Create
        public IActionResult Create()
        {
            ViewData["CodCli"] = new SelectList(_context.Clientes, "CodCli", "Documento");
            ViewData["CodTipoConta"] = new SelectList(_context.TipoConta, "CodTipoCta", "NomeTipoCta");
            return View();
        }

        // POST: Contum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodConta,Agencia,NumeroConta,CodigoBanco,CodCli,SaldoInicial,SaldoAtual,CodTipoConta")] Contum contum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodCli"] = new SelectList(_context.Clientes, "CodCli", "Documento", contum.CodCli);
            ViewData["CodTipoConta"] = new SelectList(_context.TipoConta, "CodTipoCta", "NomeTipoCta", contum.CodTipoConta);
            return View(contum);
        }

        // GET: Contum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contum = await _context.Conta.FindAsync(id);
            if (contum == null)
            {
                return NotFound();
            }
            ViewData["CodCli"] = new SelectList(_context.Clientes, "CodCli", "Documento", contum.CodCli);
            ViewData["CodTipoConta"] = new SelectList(_context.TipoConta, "CodTipoCta", "NomeTipoCta", contum.CodTipoConta);
            return View(contum);
        }

        // POST: Contum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodConta,Agencia,NumeroConta,CodigoBanco,CodCli,SaldoInicial,SaldoAtual,CodTipoConta")] Contum contum)
        {
            if (id != contum.CodConta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContumExists(contum.CodConta))
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
            ViewData["CodCli"] = new SelectList(_context.Clientes, "CodCli", "Documento", contum.CodCli);
            ViewData["CodTipoConta"] = new SelectList(_context.TipoConta, "CodTipoCta", "NomeTipoCta", contum.CodTipoConta);
            return View(contum);
        }

        // GET: Contum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contum = await _context.Conta
                .Include(c => c.CodCliNavigation)
                .Include(c => c.CodTipoContaNavigation)
                .FirstOrDefaultAsync(m => m.CodConta == id);
            if (contum == null)
            {
                return NotFound();
            }

            return View(contum);
        }

        // POST: Contum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contum = await _context.Conta.FindAsync(id);
            _context.Conta.Remove(contum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContumExists(int id)
        {
            return _context.Conta.Any(e => e.CodConta == id);
        }
    }
}
