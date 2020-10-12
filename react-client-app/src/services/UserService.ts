import AppConfig from "../appconfig";
import axios, { AxiosResponse } from "axios";

export interface UserLoginResponse {
  userName: string;
  userId: string;
  userFullName: string;
  token: string;
  roles: string[];
}

interface ErrorResponse {
  message:string
}

// export const authUser2 = async (login: string, password: string): Promise<UserLoginResponse> | Promise<ErrorResponse> => {
//   const url = AppConfig.apiUrl + "/api/authentication/login";
//   const postConf = {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify({ username: login, password }),
//   };
//   try {
//     const response = await fetch(url, postConf);
//     const data = await response.json();

//     if (response.status == 200) {
//       return data as UserLoginResponse;
//     }else{
//       return data as ErrorResponse;
//     }

    

//   } catch (error) {
//     console.error(error);
//     return {message: error.message} as ErrorResponse;
//   }
// };

export const authUser = (login: string, password: string) => {
  fetch(AppConfig.apiUrl + "/api/authentication/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ username: login, password }),
  })
    .then((response) => {
      if (response.status == 200) {
        response.json().then((data) => {});
      }
    })
    .catch((error) => {
      console.error("Error:", error);
    });
};

// export const authUser = (login: string, password: string) => {
//   fetch(AppConfig.apiUrl + "/api/authentication/login", {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify({ username: login, password }),
//   }).then((response) => {
//     console.log("api response:", response);
//     if (response.status == 200) {
//       return response.json().then((data) => data as number);
//     } else if (response.status == 500) {
//         response.json().then((data) => console.log(data));
//     } else {
//     }
//   });
// };
