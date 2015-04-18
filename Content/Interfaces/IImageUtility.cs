using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BootstrapVillas.Models;
using BootstrapVillas.Content;

namespace BootstrapVillas.Content
{
    public interface IImageUtility
    {
        

        ////////////////
        //methods
        ////////////////

        //checks there is a dir with this prcvref, return true or false
        bool CheckPRCDirectoryExists(string PRCRef);

        //takes a ref string that coresponds to the folder name - eg 'PRC01 and returns a List of IPRCVImages
        List<IPRCVImage> GetAllImagesFromADirectoryAndPlaceInACollection();
        
        

    }


    //to hold image data
    public interface IPRCVImage
    {

        


    }
}
