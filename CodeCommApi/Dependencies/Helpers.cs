using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Response;

namespace CodeCommApi.Dependencies
{
    public class Helpers<T>
    {
        public  DefaultResponse<T> ConvertToGood(String message){
            return new Response.DefaultResponse<T>(){
                Status=true,
                ResponseCode="00",
                ResponseMessage=message,
                      
            };
        }


    


        public  DefaultResponse<T> ConvertToBad(String message){
            return new Response.DefaultResponse<T>(){
                Status=false,
                ResponseCode="99",
                ResponseMessage=message,
                      
            };
        }
    }
}