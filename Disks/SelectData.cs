using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disks
{
    class SelectData
    {
        public static List<Disk> SelectY(List <Disk> disks, string company)
        {
            List<Disk> selectedDisks = new List<Disk>();
            foreach (Disk disk in disks)
            {
                if (disk.company.Equals(company))
                {
                    selectedDisks.Add(disk);
                }
            }
            return selectedDisks;
        }
        public static List<Disk> SelectXY(List<Disk> disks, string company, string type)
        {
            List<Disk> selectedDisks = new List<Disk>();
            foreach (Disk disk in SelectY(disks, company))
            {
                if (disk.type.Equals(type))
                {
                    selectedDisks.Add(disk);
                }
            }
            return selectedDisks;
        }
    }
}
