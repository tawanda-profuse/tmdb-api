import React, { useState, useEffect } from "react";

const ScrollToTopButton: React.FC = () => {
  const [showScrollTop, setShowScrollTop] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setShowScrollTop(window.scrollY > 200);
    };

    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  const scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  return (
    <>
      {showScrollTop && (
        <button
          onClick={scrollToTop}
          className="btn btn-danger"
          style={{
            position: "fixed",
            bottom: "30px",
            right: "30px",
            borderRadius: "50%",
            width: "50px",
            height: "50px",
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            fontSize: "30px",
            zIndex: 1000,
            boxShadow: "0 4px 8px rgba(0, 0, 0, 0.2)",
          }}
          title="Scroll to top"
        >
          ↑
        </button>
      )}
    </>
  );
};

export default ScrollToTopButton;
