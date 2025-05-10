Window.deviceInfo = {
    getFormFactor: function () {
        const width = window.innerWidth;
        if (width <= 768) return "Phone";
        if (width <= 1024) return "Tablet";
        return "Desktop";
    }
}