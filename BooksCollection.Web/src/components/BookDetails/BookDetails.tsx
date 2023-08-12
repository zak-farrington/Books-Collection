import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { useAppDispatch } from "../../store/store";
import { deleteBook, getBooksList, modifyBook } from "../../store/booksSlice";
import { Book, BookCategory, defaultBook } from "../../models/Book";
import { ModifyBookRequest, ModifyBookResponse } from "../../models/ModifyBook";
import { DeleteBookResponse } from "../../models/DeleteBook";
import BookForm from "../BookForm/BookForm";
import "./BookDetails.less";

const BookDetails: React.FC<{ book: Book, onClose: () => void }> = ({ book, onClose }) => {
    useEffect(() => {
        const bookUid = book?.uid;
        if ((bookUid?.length || 0) > 0) {
            setLocalBook(book);
        }
        else {
            alert("There was a problem viewing book details.");
            onClose();
        }
    }, [])

    const [isReadOnly, setIsReadOnly] = useState<boolean>(true);
    const [localBook, setLocalBook] = useState<Book | undefined>(defaultBook);
    const dispatch = useAppDispatch();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setLocalBook({ ...localBook, [name]: value });
    };

    const handleCategoryChange = (value: BookCategory) => {
        setLocalBook({ ...localBook, category: value });
    };

    const handleUpdateBookClick = async () => {
        const modifyBookRequest = {
            book: localBook,
        } as ModifyBookRequest;

        const dispatchResp = await dispatch(modifyBook(modifyBookRequest));
        const modifyBookResponse = dispatchResp.payload as ModifyBookResponse;

        if (modifyBookResponse.isSuccessful) {
            await dispatch(getBooksList());
            setIsReadOnly(true);
            alert("Book updated successfully.")
        } else {
            alert(modifyBookResponse.errorMessage || "An error occurred.");
        }
    };

    const handleDeleteBookClick = async () => {
        const uid = localBook?.uid as string;
        if (!uid) {
            alert("Cannot delete book due to missing uid.");
            return;
        }

        const dispatchResp = await dispatch(deleteBook(uid));
        const deleteBookResponse = dispatchResp.payload as DeleteBookResponse;
        if (deleteBookResponse.isSuccessful) {
            await dispatch(getBooksList());
            onClose();
            alert("Book successfully deleted.");
        } else {
            alert(deleteBookResponse.errorMessage || "An error occurred.");
        }
    };

    const handleCancelClick = () => {
        setLocalBook(book);
        setIsReadOnly(true);
    }

    const bookDetailsFormTsx = (
        <BookForm book={localBook} onInputChange={handleInputChange} onCategoryChange={handleCategoryChange} isReadOnly={isReadOnly} />
    );

    return (
        <div>
            {bookDetailsFormTsx}
            <div className="bookDetailsButtonContainer">
                {isReadOnly ?
                    (
                        <div>
                            <Button onClick={() => setIsReadOnly(false)}><i className="bi bi-pencil-square"></i>Edit Details</Button>
                            <Button onClick={handleDeleteBookClick} variant="danger"><i className="bi bi-trash"></i> Delete</Button>
                        </div>
                    ) : (
                        <div>
                            <Button onClick={handleCancelClick}>Cancel</Button>
                            <Button variant="success" onClick={handleUpdateBookClick}><i className="bi bi-save"></i> Update</Button>
                        </div>
                    )}
            </div>
        </div>
    );
};

export default BookDetails;
