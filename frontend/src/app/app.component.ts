import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ContactService, ContactPayload } from './contact.service';

interface SheetNavItem {
  id: string;
  sheetNo: string;
  label: string;
}

interface StackGroup {
  icon: string;
  label: string;
  items: string[];
}

interface ProjectSheet {
  sheetNo: string;
  title: string;
  description: string;
  specs: string[];
  rev: string;
  url?: string;
}

type FormStatus = 'idle' | 'sending' | 'success' | 'error';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(private contactService: ContactService) {}

  readonly navItems: SheetNavItem[] = [
    { id: 'home', sheetNo: '01', label: 'OVERVIEW' },
    { id: 'about', sheetNo: '02', label: 'NOTES' },
    { id: 'stack', sheetNo: '03', label: 'MATERIALS' },
    { id: 'projects', sheetNo: '04', label: 'PROJECTS' },
    { id: 'contact', sheetNo: '05', label: 'REVISIONS' },
  ];

  readonly facts = [
    { value: 'C# / .NET', label: 'CORE STACK' },
    { value: 'FULL-STACK', label: 'SPECIALIZATION' },
  ];

  readonly generalNotes: string[] = [
    'I design and build backend services in ASP.NET Core: clean domain models, well-thought-out API contracts, and error handling with no surprises.',
    'I work with relational databases (SQL Server, PostgreSQL) and EF Core — from schema design to query optimization.',
    'I build frontends in Angular — from mockup to production, with close attention to state, forms, and performance.',
    'I containerize and deploy with Docker, set up CI/CD, and make sure what works locally works in production too.',
    'I write code as if someone else will read it a year from now — including tests and documentation.',
  ];

  readonly stackGroups: StackGroup[] = [
    {
      icon: 'backend',
      label: 'BACKEND',
      items: ['C#', '.NET 8', 'ASP.NET Core', 'Web API', 'Entity Framework Core', 'LINQ'],
    },
    {
      icon: 'frontend',
      label: 'FRONTEND',
      items: ['Angular', 'React', 'TypeScript', 'JavaScript', 'HTML', 'CSS'],
    },
    {
      icon: 'database',
      label: 'DATABASE',
      items: ['SQLite', 'Entity Framework Core'],
    },
    {
      icon: 'tools',
      label: 'TOOLS',
      items: ['Git', 'GitHub', 'Visual Studio', 'Docker', 'Swagger'],
    },
  ];

  readonly projects: ProjectSheet[] = [
    {
      sheetNo: 'A-01',
      title: 'Portfolio Platform',
      description: 'This site: an Angular frontend paired with an ASP.NET Core API exposing a contact form endpoint with validation and logging.',
      specs: ['ASP.NET Core', 'Angular', 'REST API'],
      rev: 'REV A',
      url: 'https://github.com/Shimori-afk/Portfolio'
    },
    {
      sheetNo: 'A-02',
            title: 'Job Tracker',
                  description: 'Replace with a real case study: what problem it solves, the stack behind it, and your role on the team.',
                        specs: ['C#', 'SQL Server', 'Docker'],
                              rev: 'REV A',
                                    url: 'https://github.com/Shimori-afk/Job-tracker'
                                        },
    {
      sheetNo: 'A-03',
      title: 'Project Sample #3',
      description: 'Another slot for a real case study: a microservice, a third-party API integration, or an internal tool.',
      specs: ['ASP.NET Core', 'Angular', 'CI/CD'],
      rev: 'REV A',
      url: 'https://github.com/Shimori-afk'
    },
  ];

  activeNav = 'home';
  private observer?: IntersectionObserver;

  form: ContactPayload = { name: '', email: '', message: '' };
  formStatus: FormStatus = 'idle';
  formErrorMessage = '';
  currentYear = new Date().getFullYear();

  ngOnInit(): void {
    this.setupSectionObserver();
  }

  ngOnDestroy(): void {
    this.observer?.disconnect();
  }

  private setupSectionObserver(): void {
    const sections = this.navItems.map(n => document.getElementById(n.id)).filter(Boolean) as HTMLElement[];
    this.observer = new IntersectionObserver(
      entries => {
        const visible = entries
          .filter(e => e.isIntersecting)
          .sort((a, b) => b.intersectionRatio - a.intersectionRatio)[0];
        if (visible) {
          this.activeNav = visible.target.id;
        }
      },
      { rootMargin: '-30% 0px -55% 0px', threshold: [0, 0.25, 0.5, 1] }
    );
    sections.forEach(s => this.observer!.observe(s));
  }

  scrollTo(id: string): void {
    const reducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
    document.getElementById(id)?.scrollIntoView({ behavior: reducedMotion ? 'auto' : 'smooth', block: 'start' });
  }

  submitContactForm(): void {
    if (!this.form.name || !this.form.email || !this.form.message) {
      this.formStatus = 'error';
      this.formErrorMessage = 'Please fill in all fields.';
      return;
    }

    this.formStatus = 'sending';
    this.formErrorMessage = '';

    this.contactService.send(this.form).subscribe({
      next: () => {
        this.formStatus = 'success';
        this.form = { name: '', email: '', message: '' };
      },
      error: () => {
        this.formStatus = 'error';
        this.formErrorMessage = 'Failed to send. Please try again in a moment.';
      }
    });
  }
}
