import React from 'react';
import '../assets/errormessage.css';

interface ErrorMessageProps {
    message: string;
}

const ErrorMessage: React.FC<ErrorMessageProps> = ({ message }) => {
    return (
        <div className="error-message">
            <h2>Error</h2>
            <p>{message}</p>
        </div>
    );
};

export default ErrorMessage;
