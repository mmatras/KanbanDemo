using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoKanban.Models;

namespace DemoKanban.Controllers
{
    public class AuditLogsController : Controller
    {
        private readonly KanbanContext _context;

        public AuditLogsController(KanbanContext context)
        {
            _context = context;
        }

        // GET: AuditLogs
        public async Task<IActionResult> Index()
        {
              return View(await _context.AuditLog.ToListAsync());
        }

        // GET: AuditLogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AuditLog == null)
            {
                return NotFound();
            }

            var auditLog = await _context.AuditLog
                .FirstOrDefaultAsync(m => m.Id == id);

            if (auditLog == null)
            {
                return NotFound();
            }

            return View(auditLog);
        }

        // GET: AuditLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuditLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Path,Route,Timestamp,User")] AuditLog auditLog)
        {
            if (ModelState.IsValid)
            {
                auditLog.Id = Guid.NewGuid();
                _context.Add(auditLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auditLog);
        }

        // GET: AuditLogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AuditLog == null)
            {
                return NotFound();
            }

            var auditLog = await _context.AuditLog.FindAsync(id);
            if (auditLog == null)
            {
                return NotFound();
            }
            return View(auditLog);
        }

        // POST: AuditLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Path,Route,Timestamp,User")] AuditLog auditLog)
        {
            if (id != auditLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditLogExists(auditLog.Id))
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
            return View(auditLog);
        }

        // GET: AuditLogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AuditLog == null)
            {
                return NotFound();
            }

            var auditLog = await _context.AuditLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditLog == null)
            {
                return NotFound();
            }

            return View(auditLog);
        }

        // POST: AuditLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AuditLog == null)
            {
                return Problem("Entity set 'KanbanContext.AuditLog'  is null.");
            }
            var auditLog = await _context.AuditLog.FindAsync(id);
            if (auditLog != null)
            {
                _context.AuditLog.Remove(auditLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditLogExists(Guid id)
        {
          return _context.AuditLog.Any(e => e.Id == id);
        }
    }
}
