using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EleicaoBrasilApi.Data;
using EleicaoBrasilApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace EleicaoBrasilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatosController : ControllerBase
    {
       private readonly AppDbContext _context;
        private string nomeDoPartido;

        public CandidatosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var candidatos = _context.Candidatos.ToList();
            _context.Candidatos.Where(c => c.Partido == nomeDoPartido).ToList();
            
            return Ok(candidatos);
        }

            [HttpPost]
            
        public IActionResult Post(Candidato candidato)
        {            _context.Candidatos.Add(candidato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = candidato.Id }, candidato);
            
            // if (Id == candidato.Id)
            {
                return BadRequest("Numero Já Cadastrado");
            }   
        }
        [HttpPut("{id}")]
    public IActionResult Put(int id, Candidato candidatoAtualizado)
{
    if (id != candidatoAtualizado.Id)
    {
        return BadRequest();
    }

    var candidato = _context.Candidatos.Find(id);

    if (candidato == null)
    {
        return NotFound();
    }

    // Atualiza os campos manualmente
    candidato.Nome = candidatoAtualizado.Nome;
    candidato.Partido = candidatoAtualizado.Partido;
    candidato.ViceNome = candidatoAtualizado.ViceNome; // ✅ novo campo

    _context.SaveChanges();

    return NoContent();
}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)   
        {
            var candidato = _context.Candidatos.Find(id);
            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidatos.Remove(candidato);
            _context.SaveChanges();
            return NoContent();
        }
    }
}