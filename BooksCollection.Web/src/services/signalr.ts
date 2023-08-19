import * as signalR from '@microsoft/signalr';
import appConfig from "../appConfig";
import { SIGNALR_MESSAGES } from '../utils/constants';
import { Book } from '../models/Book';

const signalRConnection = new signalR.HubConnectionBuilder()
    .withUrl(appConfig.hubUrl, { withCredentials: true })
    .build();

export const registerSignalREvents = (dispatch: any, actions: any) => {
    signalRConnection.on(SIGNALR_MESSAGES.BOOK_ADDED, (addedBook: Book) => {
        dispatch(actions.addBookToStore(addedBook));
    });

    signalRConnection.on(SIGNALR_MESSAGES.BOOK_MODIFIED, (modifiedBook: Book) => {
        dispatch(actions.modifyBookInStore(modifiedBook));
    });

    signalRConnection.on(SIGNALR_MESSAGES.BOOK_REMOVED, (uid: string) => {
        dispatch(actions.removeBookFromStore(uid));
    });

    signalRConnection.start().catch(err => console.error(err));
};

export const unregisterSignalREvents = () => {
    signalRConnection.off(SIGNALR_MESSAGES.BOOK_ADDED);
    signalRConnection.off(SIGNALR_MESSAGES.BOOK_MODIFIED);
    signalRConnection.off(SIGNALR_MESSAGES.BOOK_REMOVED);
    signalRConnection.stop();
};
