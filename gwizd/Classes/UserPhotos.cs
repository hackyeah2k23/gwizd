using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwizd.Classes
{
    public class UserPhotos
    {
        public async Task<FileResult> TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                
                if (photo != null)
                {
                    return photo;
                }
                return null;
            }
            return null;
        }
    }
}
