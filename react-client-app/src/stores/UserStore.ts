import { observable, action, makeObservable, computed } from "mobx";
import { authUser, UserLoginResponse } from "../services/UserService";
import AppConfig from "../appconfig";

export interface UserData {
  id:string;
  username: string;
  userFullName: string;
  roles: string[];
}

export default class UserStore {
  constructor() {
    makeObservable(this);
  }

  @observable
  user: UserData = {} as UserData;

  @observable
  public authToken: string | null = null;

  @computed
  public get isUserLogged(){ return !!this.user?.username && !!this.authToken};

  @computed
  public get isModerator(){ return !!this.user?.roles?.some(r => r === "Moderator" || r === "Administrator") && this.isUserLogged};

  @action
  public logout = () =>{
    this.user.username = '';
    this.authToken = null;
  }

  @action
  authorizeUser = (login: string, password: string) => {

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
              userFullName: dataLogin.userFullName,
              roles: dataLogin.roles,
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
