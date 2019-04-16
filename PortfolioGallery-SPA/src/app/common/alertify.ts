// global variable from external
declare let alertify: any;

export class Alertify {
    static error(message: string) {
        alertify.error(message);
    }

    static success(message: string) {
        alertify.success(message);
    }
}
