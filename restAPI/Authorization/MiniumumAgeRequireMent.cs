using Microsoft.AspNetCore.Authorization;

namespace restAPI.Authorization
{
    public class MiniumumAgeRequireMent : IAuthorizationRequirement
    {
        private int MinimumAge { get; set; }

        public MiniumumAgeRequireMent(int v)
        {
            MinimumAge = v;
        }
    }
}