import React from "react";
import Modal from "react-modal";

Modal.setAppElement("#root");

interface ConfirmationModalProps {
  isOpen: boolean;
  onConfirm: () => void;
  onCancel: () => void;
}

const modalStyles = {
  content: {
    maxWidth: "350px",
    margin: "auto",
    padding: "2rem",
    borderRadius: "10px",
    textAlign: "center" as const,
    boxShadow: "0 2px 16px rgba(0,0,0,0.15)",
  },
  overlay: {
    backgroundColor: "rgba(0,0,0,0.3)",
  },
};

const ConfirmationModal: React.FC<ConfirmationModalProps> = ({
  isOpen,
  onConfirm,
  onCancel,
}) => (
  <Modal isOpen={isOpen} style={modalStyles}>
    <h2>Set High Priority?</h2>
    <p>
      Are you sure you want to set this task to <b>High priority</b>?
    </p>
    <div style={{ marginTop: "2rem", display: "flex", gap: "1rem", justifyContent: "center" }}>
      <button
        style={{
          background: "#1976d2",
          color: "#fff",
          border: "none",
          borderRadius: "5px",
          padding: "0.5rem 1.5rem",
          cursor: "pointer",
        }}
        onClick={onConfirm}
      >
        Yes
      </button>
      <button
        style={{
          background: "#eee",
          color: "#333",
          border: "none",
          borderRadius: "5px",
          padding: "0.5rem 1.5rem",
          cursor: "pointer",
        }}
        onClick={onCancel}
      >
        No
      </button>
    </div>
  </Modal>
);

export default ConfirmationModal;