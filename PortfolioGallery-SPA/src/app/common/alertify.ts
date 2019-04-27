// global variable from external
declare const alertify: any;

export class Alertify {
    static error(message: string) {
        alertify.error(message);
    }

    static success(message: string) {
        alertify.success(message);
    }

    static warning(message: string) {
        alertify.warning(message);
    }
}
