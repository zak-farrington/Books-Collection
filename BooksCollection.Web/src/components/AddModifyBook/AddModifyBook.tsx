import React, { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useAppDispatch } from "../../store/store";
import { addBook, getBooksList, searchGoogleBooks, selectGoogleSearchBooks } from "../../store/booksSlice";
import { bookThumbnailUnavailableSrc } from "../../utils/constants";
import { Book, BookCategory, defaultBook } from "../../models/Book";
import { AddBookRequest, AddBookResponse } from "../../models/AddBook";
import BookForm from "../BookForm/BookForm";
import "./AddModifyBook.less";

const AddModifyBookForm: React.FC<{ onClose: () => void }> = ({ onClose }) => {
    const [localBook, setLocalBook] = useState<Book | undefined>(defaultBook);
    const [showGoogleSearchForm, setShowGoogleSearchForm] = useState(false);
    const [searchTitle, setSearchTitle] = useState("");
    const dispatch = useAppDispatch();
    const searchResults = useSelector(selectGoogleSearchBooks);


    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setLocalBook({ ...localBook, [name]: value });
    };

    const handleCategoryChange = (value: BookCategory) => {
        setLocalBook({ ...localBook, category: value });
    };

    const handleSearchSubmitClick = (e: React.FormEvent) => {
        e.preventDefault();
        dispatch(searchGoogleBooks(searchTitle));
    };

    const handleSearchResultClick = (resultBook: Book) => {
        setLocalBook({
            ...resultBook
        });
        setShowGoogleSearchForm(false);
    };

    const handleSaveBook = async () => {
        const saveBookRequest = {
            book: localBook,
        } as AddBookRequest;

        const dispatchResp = await dispatch(addBook(saveBookRequest));
        const addBookResponse = dispatchResp.payload as AddBookResponse;

        if (addBookResponse.isSuccessful) {
            await dispatch(getBooksList());
            onClose();
            alert("Book added successfully.");
        } else {
            alert(addBookResponse.errorMessage || "An error occurred.");
        }
    };

    const googleSearchFormTsx = (
        <div>
            <Form onSubmit={handleSearchSubmitClick} className="googleSearchForm">
                <Form.Group>
                    <Form.Label>Search Google for Title</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter a title and hit enter"
                        value={searchTitle}
                        onChange={(e) => setSearchTitle(e.target.value)}
                    />
                </Form.Group>
            </Form>
            <div className="googleBookResults">
                {searchResults.map((resultBook: Book, index: number) => (
                    <div key={index} className="googleBookResultItem" onClick={() => handleSearchResultClick(resultBook)}>
                        <img src={resultBook.imageUrl ?? bookThumbnailUnavailableSrc} alt={resultBook.title} className="googleSearchThumbnail" title="Click to prepopulate fields." />
                        <div>
                            <div><strong>{resultBook.title}</strong></div>
                            <div>{resultBook.description?.slice(0, 150)}</div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );

    const addBookFormTsx = (
        <BookForm book={localBook} onInputChange={handleInputChange} onCategoryChange={handleCategoryChange} isReadOnly={false} />
    );

    return (
        <div>
            {showGoogleSearchForm ? googleSearchFormTsx : addBookFormTsx}
            <div className="addBookButtonContainer">
                <Button onClick={() => setShowGoogleSearchForm(!showGoogleSearchForm)}>
                    {showGoogleSearchForm ? "Return to Manual Entry" : "Search Google Books"}
                </Button>
                <Button variant="success" onClick={handleSaveBook}><i className="bi bi-save"></i> Save</Button>
            </div>
        </div>
    );
};

export default AddModifyBookForm;
