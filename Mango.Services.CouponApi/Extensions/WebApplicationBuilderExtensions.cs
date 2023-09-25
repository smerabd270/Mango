using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Services.CouponApi.Extensions
{
	public  static class WebApplicationBuilderExtensions
	{
		public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder) 
		{
			var settingSection = builder.Configuration.GetSection("ApiSettings");
			var secret = settingSection.GetValue<string>("Secret");
			var issuer = settingSection.GetValue<string>("Issuer");
			var aduience = settingSection.GetValue<string>("Aduience");
			var key = Encoding.ASCII.GetBytes(secret);
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateAudience = true,
					ValidAudience = aduience,
					ValidIssuer = issuer,
					ValidateIssuer = true,

				};
			});
			return builder;
		}
	}
}
