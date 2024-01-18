using EnglishService.Data;
using EnglishService.Models;
using EnglishService.Repository;
using EnglishService.Services.IServices;
using EnglishService.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Core.Types;
using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using EnglishService.Extensions;
using AutoMapper;

namespace EnglishService.Services
{
    public class ProfessionalService: IProfessionalService
    {
        private readonly AppDbContext _appDbContext;

        private readonly IMapper _mapper;

        private readonly IRepository<Professional>  _professionalRepository;
        public ProfessionalService(AppDbContext appDbContext, IRepository<Professional> professionalRepository, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _professionalRepository = professionalRepository;
            _mapper = mapper;
        }
        public async Task UpdateAsync(ProfessionalEditVM model, string imagePath)
        {
            var professional = GetProfessionalById(model.Id);

            professional.FirstName = model.FirstName;
            professional.LastName = model.LastName;
            professional.Age = model.Age;
            professional.PhoneNumber = model.PhoneNumber;
            professional.Experience = model.Experience;
            professional.Email = model.Email;
            professional.Address = model.Address;
            professional.Resume = model.Resume;
            professional.RegionId = model.RegionId;
            professional.SpecializationId = model.SpecializationId;

            if (model.Image != null)
            {
                var image = await _appDbContext.Images.FirstOrDefaultAsync(i => i.ProfessionalId == model.Id);

                // couldn't figure out a realistic way to delete seeded doctors image
                if (image.ImageUrl == null)
                {
                    File.Delete(imagePath + model.ImageUrl);
                }

                _appDbContext.Remove(image);

                Directory.CreateDirectory($"{imagePath}/img/professionals/");

                var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

                var dbImage = new Image
                {
                    Extension = extension,
                };
                professional.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/img/professionals/{dbImage.Id}.{extension}";

                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

                await model.Image.CopyToAsync(fileStream);
            }

            _appDbContext.Update(professional);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(ProfessionalCreateVM model, string imagePath)
        {
            var professional = new Professional()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                Experience = model.Experience,
                Email = model.Email,
                Address = model.Address,
                Resume = model.Resume,
                RegionId = model.RegionId,
                SpecializationId = model.SpecializationId,
                UserId = model.UserId,
                IsApplied = true
            };
            Directory.CreateDirectory($"{imagePath}/professionals/");

            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            
            var dbImage = new Image
            {
                Extension = extension,
            };

           
            var physicalPath = $"{imagePath}/professionals/{dbImage.Id}.{extension}";
            dbImage.ImageUrl = physicalPath;
            professional.Images.Add(dbImage);
            await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

            await model.Image.CopyToAsync(fileStream);

            await _appDbContext.AddAsync(professional);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<ProfessionalInfoListVM> GetAllValidatedProfessionals(int page, int itemsPerPage)
        {
            var model = _appDbContext.Professionals
                .Where(d => d.IsApproved)
                .OrderBy(d => d.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
            return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
           
        }

        public IEnumerable<ProfessionalInfoListVM> GetAllValidatedProfessionals(int page, int itemsPerPage, string searchContent, int? regionId, int? specializationId)
        {
            // IEnumerable<ProfessionalInfoListVM> model = null;
            List<Professional> model;
            if (!string.IsNullOrWhiteSpace(searchContent) && regionId > 0 && specializationId > 0)
            {
                model = _appDbContext.Professionals
                    .Where(p => (p.FirstName + " " + p.LastName).Contains(searchContent) && p.RegionId == regionId && p.SpecializationId == specializationId)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
                return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
            }
            else if (!string.IsNullOrWhiteSpace(searchContent) && regionId > 0)
            {
                model = _appDbContext.Professionals
                    .Where(p => (p.FirstName + " " + p.LastName).Contains(searchContent) && p.RegionId == regionId)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
                return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
            }
            else if (regionId > 0)
            {
                model = _appDbContext.Professionals
                    .Where(p => p.RegionId == regionId)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
                return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
            }
            else if (specializationId > 0)
            {
                 model = _appDbContext.Professionals
                    .Where(p => p.SpecializationId == specializationId)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
                return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
            }
            else if (!string.IsNullOrWhiteSpace(searchContent) && specializationId > 0)
            {
                 model = _appDbContext.Professionals
                    .Where(p => p.IsApproved && (p.FirstName + " " + p.LastName).Contains(searchContent) && p.SpecializationId == specializationId)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
                return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
            }
            else if (!string.IsNullOrWhiteSpace(searchContent))
            {
                 model = _appDbContext.Professionals
                     .Where(p => (p.FirstName + " " + p.LastName).Contains(searchContent))
                     .OrderBy(p => p.Id)
                     .Skip((page - 1) * itemsPerPage)
                     .Take(itemsPerPage)
                     .ToList();
                return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
            }

            return null;
        }

        public IEnumerable<T> GetAllAppliedProfessionals<T>(int page, int itemsPerPage)
        {
            var model = _professionalRepository.AllAsNoTracking()
                .Where(p => p.IsApplied && p.IsApproved == false)
                .OrderBy(d => d.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return model;
        }

        public int GetAppliedAndNotValidatedProfessionalsCount() => _appDbContext.Professionals.Count(d => d.IsApplied && d.IsApproved == false);

        public int? GetProfessionalIdByUserId(string userId) => _appDbContext.Professionals.FirstOrDefault(p => p.UserId == userId)?.Id;

        public T GetProfessionalById<T>(int professionalId)
        {
            return _professionalRepository.All().Where(d => d.Id == professionalId).To<T>().FirstOrDefault();
        }

        public Professional GetProfessionalById(int professionalId) => _professionalRepository.All().FirstOrDefault(p => p.Id == professionalId);

        public async Task<bool> VerifyAsync(int professionalId, string userId)
        {
            var professional = GetProfessionalById(professionalId);
            var professionalUser = _appDbContext.AppUsers.FirstOrDefault(u => u.Id == userId);
            if (professional == null)
            {
                return false;
            }

            professional.IsApproved = true;
            professionalUser.Professional = professional;

            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int professionalId)
        {
            var doctor = GetProfessionalById(professionalId);

            if (doctor == null)
            {
                return false;
            }

            _appDbContext.Remove(doctor);

            await _appDbContext.SaveChangesAsync();
            return true;
        }


        public ProfessionalInfoListVM GetProfessionalListById(int doctorId)
        {
            var professional = _appDbContext.Professionals.FirstOrDefault(d => d.Id == doctorId);
            var professionalList = new ProfessionalInfoListVM()
            {
                UserId = professional.UserId,
                FullName = professional.FirstName + " " + professional.LastName,
                //ImageUrl=professional.Images,
                Age = professional.Age,
                PhoneNumber = professional.PhoneNumber,
                Experience = professional.Experience,
                Email = professional.Email,
                Address = professional.Address,
                Resume = professional.Resume,
                RegionName = professional.Region.Name,
                SpecializationName = professional.Specialization.Name,
                IsApplied = professional.IsApplied,
                IsApproved = professional.IsApproved,
            };
            return professionalList;
        }
        public ProfessionalEditVM GetProfessionalForUpdate(int professionalId)
        {
            var professional = _appDbContext.Professionals.FirstOrDefault(d => d.Id == professionalId);
            
            //var professionalEdit = new ProfessionalEditVM()
            //{
            //    Id = professional.Id,
            //    FirstName = professional.FirstName,
            //    LastName = professional.LastName,
            //    Image = i,
            //    Age = professional.Age,
            //    PhoneNumber = professional.PhoneNumber,
            //    Experience = professional.Experience,
            //    Email = professional.Email,
            //    Address = professional.Address,
            //    Resume = professional.Resume,
            //    RegionId = professional.Region.Id,
            //    SpecializationId = professional.Specialization.Id,

            //};
            return _mapper.Map<ProfessionalEditVM>(professional);
        }

        public IEnumerable<ProfessionalInfoListVM> GetAllAppliedProfessionals(int page, int itemsPerPage)
        {
            var model = _appDbContext.Professionals
                .Where(p => p.IsApplied && p.IsApproved == false)
                .OrderBy(d => d.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            return _mapper.Map<IEnumerable<ProfessionalInfoListVM>>(model);
        }
    }

}
