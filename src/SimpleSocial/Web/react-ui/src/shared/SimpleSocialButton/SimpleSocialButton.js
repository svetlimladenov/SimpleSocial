import React from "react";
import styles from "./simpleSocialButton.module.css";

function SimpleSocialButton({ children, handleClick }) {
  return (
    <div>
      <button className={styles["simple-social-button"]} onClick={handleClick}>
        {children}
      </button>
    </div>
  );
}

export default SimpleSocialButton;
