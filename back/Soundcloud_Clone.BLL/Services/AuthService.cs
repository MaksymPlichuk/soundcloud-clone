using Microsoft.AspNetCore.Identity;
using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Mapperly;
using Soundcloud_Clone.DAL.Enitites.Identity;
using Soundcloud_Clone.DAL.Repositories;

namespace Soundcloud_Clone.BLL.Services
{
    public class AuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly UserManager<UserEntity> _userManager;
        private readonly MapperProfile _mapper;
        private readonly TokenService _tokenService;

        public AuthService(AuthRepository authRepository, UserManager<UserEntity> userManager, MapperProfile mapper, TokenService tokenService)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var res = (await _authRepository.CheckUserExists(dto.Email));
            if (res == null)
            {
                return ServiceResponse.Failure("Wrong Email or password");
            }
            
            var passCheck = await _userManager.CheckPasswordAsync(res, dto.Password);
            if (!passCheck)
            {
                return ServiceResponse.Failure("Wrong Email or password");
            }

            string jwt = await _tokenService.GenerateTokenAsync(res);
            return ServiceResponse.Success("Logged in Succesfully!", jwt);
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterDto dto)
        {
            var checkUser = await _userManager.FindByEmailAsync(dto.Email);
            if (checkUser != null) { return ServiceResponse.Failure("This email already exists!"); }


            var newUser = new UserEntity
            {
                UserName = dto.UserName,
                Email = dto.Email
                //Image
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded) { return ServiceResponse.Failure($"{result.Errors.FirstOrDefault()}"); }

            return ServiceResponse.Success($"User {dto.UserName} created!");

        }

    }
}