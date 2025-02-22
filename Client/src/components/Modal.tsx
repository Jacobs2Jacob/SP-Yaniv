import { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { Id } from "react-toastify/unstyled";

interface ModalProps {
    show: boolean;
    text: string;
    saveBtnText: string;
    cancelBtnText: string;
    onSave: () => void;
    onCancel: () => void;
}

const Modal = (props: ModalProps) => { 
    const [id, setId] = useState<Id>();

    // Show the toast when the modal is displayed
    useEffect(() => {

        if (props.show) {
            setId(toast.info(
                <div>
                    <p>{props.text}</p>
                    <div style={buttonContainerStyle}>
                        <button onClick={props.onSave} style={buttonStyle}>
                            {props.saveBtnText}
                        </button>
                        <button onClick={props.onCancel} style={buttonStyle}>
                            {props.cancelBtnText}
                        </button>
                    </div>
                </div>,
                { position: "top-center", autoClose: false, closeOnClick: false, closeButton: false }
            ));
        }
        else {
            toast.dismiss(id);
        }
    }, [props.show])
     
    return (
        <div>
            <ToastContainer />
        </div>
    );
};

// Button styles
const buttonStyle = {
    padding: "10px 20px",
    cursor: "pointer",
    border: "none",
    background: "#007bff",
    color: "white",
    borderRadius: "5px",
};

// Container for the buttons
const buttonContainerStyle = {
    display: "flex",
    justifyContent: "space-evenly",
    marginTop: "20px",
};

export default Modal;
