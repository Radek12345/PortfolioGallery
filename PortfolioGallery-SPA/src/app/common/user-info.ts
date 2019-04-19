export class UserInfo {

    static getLoggedUserId(): string {
        return JSON.parse(localStorage.getItem('user')).id;
    }
}
