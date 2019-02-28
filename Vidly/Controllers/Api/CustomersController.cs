using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DAL;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
	public class CustomersController : ApiController
	{
		private VidlyContext _context = new VidlyContext();

		public CustomersController()
		{
			_context = new VidlyContext();
		}

		//GET /api/customers
		public IHttpActionResult GetCustomers()
		{
			var customerDtos = _context.Customers.Include(c => c.MembershipType).ToList().Select(Mapper.Map<Customer, CustomerDTO>);

			return Ok(customerDtos);
		}

		//GET /api/customers/1
		public IHttpActionResult GetCustomer(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				return NotFound();
			}

			return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
		}

		//POST /api/customers
		[HttpPost]
		public IHttpActionResult CreateCustomer(CustomerDTO customerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);

			_context.Customers.Add(customer);
			_context.SaveChanges();

			customerDto.Id = customer.Id;

			return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
		}

		//PUT /api/customers/1
		[HttpPut]
		public void UpdateCustomer(int id, CustomerDTO customerDto)
		{
			if (!ModelState.IsValid)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			Mapper.Map(customerDto, customer);

			_context.SaveChanges();
		}

		//DELETE /api/customers/1
		[HttpDelete]
		public void DeleteCustomer(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			_context.Customers.Remove(customer);
			_context.SaveChanges();
		}
	}
}
