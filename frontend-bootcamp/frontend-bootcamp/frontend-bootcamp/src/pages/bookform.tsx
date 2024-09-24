import { useState, useEffect } from 'react';
import { useParams, useNavigate, useLocation } from "react-router-dom";
import '../assets/authors.css';
import fetcher from '../Fetcher';
import '../assets/form.css';

function BookForm() {
    const [book, setBook] = useState<Book>(() => ({ id: 0, title: '', yearPublished: 0, authorId: 0 }));
    const { authorId, id } = useParams<{ authorId: string; id?: string }>();
    const navigate = useNavigate();
    const location = useLocation();
    const onBookAdded = location.state?.onBookAdded;

    interface Book {
        id: number;
        title: string;
        yearPublished: number;
        authorId: number;
    }

    useEffect(() => {
        fetchInfo();
    }, []);

    function fetchInfo() {
        if (!id) {
            setBook({
                id: 0,
                title: '',
                yearPublished: 0,
                authorId: parseInt(authorId!)
            });
        } else {
            fetcher<Book>(`https://localhost:44371/authors/${authorId}/books/${id}`)
                .then(data => setBook(data));
        }
    }

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setBook(prevBook => ({ ...prevBook, [name]: value }));
    };

    function handleSubmit() {
        if (!id) {
            fetcher<Book>(`https://localhost:44371/authors/${authorId}/books`, 'POST', book)
                .then(() => {
                    onBookAdded && onBookAdded();
                    navigate(`/authors/${authorId}`);
                });
        } else {
            fetcher<Book>(`https://localhost:44371/authors/${authorId}/books/${id}`, 'PUT', book)
                .then(() => {
                    onBookAdded && onBookAdded();
                    navigate(`/authors/${authorId}`);
                });
        }
    }

    return (
        <div className='form'>
            <div className='form-title'>Book Form</div>
            <div className='form-subtitle'>Enter the info for the Book</div>
            <div>
                <div className='input-container ic1'>
                    <input className='input' type="text" name="title" value={book.title}
                        onChange={handleChange} />
                    <div className='cut'></div>
                    <label className='placeholder'>Title</label>
                </div>
                <div className='input-container ic2'>
                    <input className='input' type="text" name="yearPublished" value={book.yearPublished}
                        onChange={handleChange} />
                    <div className='cut'></div>
                    <label className='placeholder'>Year Published</label>
                </div>
                <button className='form-submit' onClick={handleSubmit}>Submit</button>
            </div>
        </div>
    );
}

export default BookForm;
