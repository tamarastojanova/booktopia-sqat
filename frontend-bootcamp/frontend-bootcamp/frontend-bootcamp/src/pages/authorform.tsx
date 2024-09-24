import { useState, useEffect } from 'react';
import { useParams, useNavigate } from "react-router-dom";
import '../assets/authors.css';
import fetcher from '../Fetcher';
import '../assets/form.css';

function AuthorForm() {
    const [author, setAuthor] = useState<Author>(() => ({ id: 0, firstName: '', lastName: '', imageUrl: '' }));
    let { id } = useParams<string>();
    const navigate = useNavigate();

    interface Author {
        id: number;
        firstName: string;
        lastName: string;
        imageUrl: string;
    }

    useEffect(() => {
        fetchInfo();
    }, []);

    function fetchInfo() {
        if (id === undefined) {
            setAuthor({
                id: 0,
                firstName: '',
                lastName: '',
                imageUrl: ''
            });
        } else {
            fetcher<Author>(`https://localhost:44371/authors/${id}`)
                .then(data => setAuthor(data));
        }
    }

    const handleChange = (event: any) => {
        const { name, value } = event.target;
        setAuthor(prev => ({ ...prev, [name]: value }));
    };

    async function handleSubmit() {
        if (id === undefined) {
            // Add new author
            await fetcher<Author>('https://localhost:44371/authors', 'POST', author);
        } else {
            // Update existing author
            await fetcher<Author>(`https://localhost:44371/authors/${id}`, 'PUT', author);
        }
        navigate('/authors'); // Redirect only after the fetcher call completes
    }

    return (
        <div className='form'>
            <div className='form-title'>Author Form</div>
            <div className='form-subtitle'>Enter the info for the Author</div>
            <div>
                <div className='input-container ic1'>
                    <input className='input' type="text" name="firstName" value={author?.firstName}
                        onChange={handleChange} />
                    <div className='cut'></div>
                    <label className='placeholder'>First Name</label>
                </div>
                <div className='input-container ic2'>
                    <input className='input' type="text" name="lastName" value={author?.lastName}
                        onChange={handleChange} />
                    <div className='cut'></div>
                    <label className='placeholder'>Last Name</label>
                </div>
                <div className='input-container ic1'>
                    <input className='input' type="text" name="imageUrl" value={author?.imageUrl}
                        onChange={handleChange} />
                    <div className='cut'></div>
                    <label className='placeholder'>Image Url</label>
                </div>
                <button className='form-submit' onClick={handleSubmit}>Submit</button>
            </div>
        </div>
    );
}

export default AuthorForm;
