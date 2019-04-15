using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using Vidly.DAL;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class CustomerController : Controller
	{
		private VidlyContext _context = new VidlyContext();

		public CustomerController()
		{
			_context = new VidlyContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Customer
		public ActionResult Index()
		{
			if (MemoryCache.Default["Genres"] == null)
			{
				MemoryCache.Default["Genres"] = _context.Genres.ToList();
			}

			var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;

			return View();
		}

		public ActionResult Details(int? id)
		{
			var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				return HttpNotFound();
			}

			return View(customer);
		}

		[ActionName("New")]
		public ActionResult Create()
		{
			var viewModel = new CustomerFormViewModel
			{
				MembershipTypes = _context.MembershipTypes.ToList(),
				Customer = new Customer()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Customer customer)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new CustomerFormViewModel
				{
					MembershipTypes = _context.MembershipTypes.ToList(),
					Customer = customer
				};

				return View("New", viewModel);
			}

			_context.Customers.Add(customer);
			_context.SaveChanges();

			return RedirectToAction("Index", "Customer");
		}

		public ActionResult Edit(int id)
		{
			var customer = _context.Customers.Include(c => c.Rentals.Select(r => r.Movie)).SingleOrDefault(c => c.Id == id);

			if (customer == null)
			{
				return HttpNotFound();
			}

			var viewModel = new CustomerFormViewModel
			{
				Customer = customer,
				MembershipTypes = _context.MembershipTypes.ToList()
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Customer customer)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new CustomerFormViewModel
				{
					MembershipTypes = _context.MembershipTypes.ToList(),
					Customer = customer
				};

				return View("Edit", viewModel);
			}

			if (customer.Id == 0)
			{
				return HttpNotFound();
			}
			else
			{
				var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

				customerInDb.Name = customer.Name;
				customerInDb.DateOfBirth = customer.DateOfBirth;
				customerInDb.MembershipTypeId = customer.MembershipTypeId;
				customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
				customerInDb.BlackFlag = customer.BlackFlag;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Customer");
		}
	}
}
