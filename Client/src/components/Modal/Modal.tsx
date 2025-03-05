import { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import styles from "./Modal.module.css";
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
                    <div className={styles.buttonContainer}>
                        <button onClick={props.onSave} className={styles.button}>
                            {props.saveBtnText}
                        </button>
                        <button onClick={props.onCancel} className={styles.button}>
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
 
export default Modal;
