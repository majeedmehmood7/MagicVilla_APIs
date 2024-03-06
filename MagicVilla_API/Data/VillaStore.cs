﻿using MagicVilla_API.Models.DTOs;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
            {
                new VillaDTO{ Id = 1,Name="Pool View",Sqft=300 , Occupancy=4},
                new VillaDTO{ Id = 2,Name="Beach View",Sqft=200 , Occupancy=5}
            };
    }
}
