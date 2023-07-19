import scrapy


class CoursesSpider(scrapy.Spider):
    name = "courses"
    allowed_domains = ["ondemand.ecornell.com"]
    start_urls = ["https://ondemand.ecornell.com/"]

    def parse(self, response):
        pass
