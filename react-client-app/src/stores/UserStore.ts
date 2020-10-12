import { observable, action, makeObservable } from "mobx";
import { authUser, UserLoginResponse } from "../services/UserService";
import AppConfig from "../appconfig";

export interface UserData {
  id: string;
  username: string;
}

export default class UserStore {
  constructor() {
    makeObservable(this);
  }

  @observable
  user: UserData = {} as UserData;

  @observable
  public authToken: string  = 'null';
  

  @action
  test = () => {
    this.user = {
      ...this.user,
      username: "test",
    };

    this.authToken = "testttt auth";
  };

  @action
  authorizeUser = (login: string, password: string) => {
    // authUser(login, password);

    fetch(AppConfig.apiUrl + "/api/authentication/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username: login, password }),
    })
      .then((response) => {
        if (response.status == 200) {
          response.json().then((data) => {
            const dataLogin = data as UserLoginResponse;
            this.user = {
              id: dataLogin.userId,
              username: dataLogin.userName,
            };
            this.authToken = dataLogin.token;
          });
        } else {
          response.json().then((data) => {
            console.info("Api response", data);
          });
        }
      })
      .catch((error) => {
        console.error("Error:", error);
        return "error";
      });

    return null;
  };
}
