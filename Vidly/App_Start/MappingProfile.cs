using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Domain to DTO
			Mapper.CreateMap<Customer, CustomerDTO>();
			Mapper.CreateMap<Movie, MovieDTO>();
			Mapper.CreateMap<MembershipType, MembershipTypeDTO>();
			Mapper.CreateMap<Genre, GenreDTO>();
			Mapper.CreateMap<Rental, RentalDTO>();

			// DTO to Domain
			Mapper.CreateMap<CustomerDTO, Customer>();
			Mapper.CreateMap<MovieDTO, Movie>();
			Mapper.CreateMap<MembershipTypeDTO, MembershipType>();
			Mapper.CreateMap<GenreDTO, Genre>();
			Mapper.CreateMap<RentalDTO, Rental>();
		}
	}
}